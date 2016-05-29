__author__ = 'Hadar'
"""
##################################################################
# Created By:  Hadar Rabin                                       #
# Date: 20/05/2016                                               #
# Name: Server  between GUI and clients                          #
# Version: 1.0                                                   #
# Windows Tested Versions: Win 7 64-bit                          #
# Python Tested Versions: 2.6 32-bit                             #
# Python Environment  : PyCharm                                  #
##################################################################
"""
import socket
from Crypto import *
import threading
import pickle
from base64 import b64decode, b64encode
import time
import sys
import struct
import sqlite3
import Aes
from Rsa import *
# region ----------   C O N S T A N T S  ------------------------------------------------------------------------------------------------
PORT = 5070
LEN_UNIT_BUF = 2048  # Min len of buffer for receive from server socket
MAX_ENCRYPTED_MSG_SIZE = 128
MAX_SOURCE_MSG_SIZE = 128
END_LINE = "\r\n"
LOCAL_ADDRESS = "0.0.0.0"
GUI_ADDRESS = "127.0.0.1"
GUI_PORT = 11555


class server():
    def __init__(self):
        self.IF_CLIENT_NOT_CONNECTED = True
        self.ServerSocket = socket.socket()
        self.client_keys = {}
        self.client_Ips = {}
        self.crypto = Crypto()
        # self.f = open(r'\\.\pipe\myPipee', 'r+b', 0)
        self.GUISocket = socket.socket()


    def connectdb(self, path):
        path = str(path)
        ACCESS_DATABASE_FILE = path + "\\Proc_database.sqlite"
        print ACCESS_DATABASE_FILE
        conn = sqlite3.connect(ACCESS_DATABASE_FILE)
        return conn

    def writeTGui(self, s):
        print s
        self.GUISocket.send(s)
        time.sleep(0.3)
        # print len(s)
        # self.f.write(struct.pack('I', len(s)) + s)
        # self.f.seek(0)

    def readFGui(self):
        datarecievedfromGUI = self.GUISocket.recv(2048)
        return datarecievedfromGUI
        # n = struct.unpack('I', self.f.read(4))[0]
        # s = self.f.read(n)
        # self.f.seek(0)
        # return s

    def key_exchange(self, client_socket):
        if self.crypto.private_key.can_encrypt():
            # --------------------  1 ------------------------------------------------------------------------
            # ------------  Send  server publicKey
            client_socket.send(pickle.dumps(self.crypto.private_key.publickey()) + END_LINE)
            time.sleep(0.5)
            # -----------  send  Base64 Hash of self.crypto.private_key.publickey()
            client_socket.send(
                b64encode(SHA256.new(pickle.dumps(self.crypto.private_key.publickey())).hexdigest()) + END_LINE)
            time.sleep(0.5)
            # --------------------  2 ------------------------------------------------------------------------
            # --------------  Wait client private key  --------------------------------------------------------
            # get Pickled private  key
            pickled_client_private_key = client_socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
            client_private_key = pickle.loads(pickled_client_private_key)

            # --------------  Wait client hash private key  ---------------------------------------------------------------------------
            # Hashing original  client private key
            calculated_hash_client_pickled_private_key = SHA256.new(pickle.dumps(client_private_key)).hexdigest()
            data = client_socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
            print '1111111111111    ', data
            declared_hash_client_pickled_private_key = b64decode(data)
            if calculated_hash_client_pickled_private_key != declared_hash_client_pickled_private_key:
                print "Error : hash and original"
                return

            client_private_key = RSA.importKey(client_private_key)

            ''' Due to a bug in pyCrypto, it is not possible to decrypt RSA messages that are longer than 128 byte.
                        To overcome this problem, the following code receives  the encrypted data in chunks of 128 byte.
                        We need to think how to tell the students about this behavior (another help message?)
                        And maybe we should implemented this approach in level 3 as well...
            '''

            # --------------------  3 ------------------------------------------------------------------------
            #  -------------- Receive from client in parts message
            #  -------------- encrypted by server public key info containing symmetric key and hash symmetric key encrypted by client public key
            pickled_client_key = ''
            pickled_encrypted_client_key = ''
            #   Recieve from client number of encrypted message parts
            msg_parts = client_socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
            for i in xrange(int(msg_parts)):
                # Wait from client current part of encrypt client_key
                part_pickled_encrypted_client_key = client_socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
                pickled_encrypted_client_key += part_pickled_encrypted_client_key
                # Decryption current part of encrypt client_key
                part_encrypt_client_key = pickle.loads(part_pickled_encrypted_client_key)
                part_pickled_client_key = self.crypto.private_key.decrypt(part_encrypt_client_key)
                pickled_client_key += part_pickled_client_key
            items = pickled_client_key.split('#')
            client_sym_key_original = b64decode(items[0])
            print 'Client Sym Key Original :     ' + client_sym_key_original
            print len(client_sym_key_original)
            # --------   Extract Client Hash Sym Key
            client_encrypted_hash_sym_key = b64decode(items[1])
            client_encrypted_hash_sym_key = pickle.loads(client_encrypted_hash_sym_key)

            splitted_client_encrypted_hash_sym_key = [client_encrypted_hash_sym_key[i:i + MAX_ENCRYPTED_MSG_SIZE] for i
                                                      in xrange(0, len(client_encrypted_hash_sym_key),
                                                                MAX_ENCRYPTED_MSG_SIZE)]
            msg_parts = len(splitted_client_encrypted_hash_sym_key)
            client_hash_sym_key = ''
            for i in xrange(int(msg_parts)):
                # Decryption current part of encrypt client_key
                part_client_encrypted_hash_sym_key = client_private_key.decrypt(
                    splitted_client_encrypted_hash_sym_key[i])
                client_hash_sym_key += part_client_encrypted_hash_sym_key
            print 'Client Hash Sym Key  :     ' + client_hash_sym_key
            calculated_client_sym_key_original = SHA256.new(client_sym_key_original).hexdigest()
            if calculated_client_sym_key_original != client_hash_sym_key:
                print "Error : hash and original"
            return client_sym_key_original

    def sendTclient(self, csocket, data):
        encrypted_data = self.client_keys[csocket].encrypt_data(data)
        csocket.send(encrypted_data)

    def recvFclient(self, csocket):
        encrypted_data = csocket.recv(LEN_UNIT_BUF)
        data = self.client_keys[csocket].decrypt_data(encrypted_data)
        return data

    def SessionWithClient(self, csocket):
        Ip = self.client_Ips[csocket]
        Accv = self.key_exchange(csocket)
        self.client_keys[csocket] = Accv
        AccvO = Aes.AESK(Accv)
        self.client_keys[csocket] = AccvO
        UUID = self.recvFclient(csocket)
        user_name = self.recvFclient(csocket)
        os_version = self.recvFclient(csocket)
        processor = self.recvFclient(csocket)
        cpus_num = self.recvFclient(csocket)
        RAM_size = self.recvFclient(csocket)
        disk_C_size = self.recvFclient(csocket)
        print "finished receiving"
        time.sleep(1.5)
        self.writeTGui("New client arrives")
        self.writeTGui(Ip)
        self.writeTGui(Ip + ":UUID:" + UUID)
        self.writeTGui(Ip + ":user name:" + user_name)
        self.writeTGui(Ip + ":processor:" + processor)
        self.writeTGui(Ip + ":cpus num:" + cpus_num)
        self.writeTGui(Ip + ":RAM size:" + RAM_size)
        self.writeTGui(Ip + ":disk C size:" + disk_C_size)

    def Continues(self,path):
        #self.dbconn = self.connectdb(path)
        time.sleep(15)
        while True:
            sockbool = True
            commend = self.readFGui()
            print commend
            Ipindex = commend.find(":")
            Ip = commend[:Ipindex]
            commandd = commend[Ipindex+1:]
            print commandd


            csocket = None
            for s in self.client_Ips.keys():
                if self.client_Ips[s] == Ip:
                    csocket = s
            if csocket == None:
                sockbool = False
            boo = True
            if sockbool == True:
                try:
                    dotsind = commandd.find(":")
                    if dotsind > 0:
                        kil = commandd[:dotsind]
                        if(kil == "Kill Process"):
                            self.sendTclient(csocket,commandd)
                            boo = False
                except:
                    pass
            if boo:
                self.work(csocket,commandd)
                # commend will be sent to a class the will translate it to a command for the client

    def work(self, csocket, command):
        Ip = self.client_Ips[csocket]
        print "command recieved"
        # if else region
        if command == "Get Total Using":
            print 'performing'
            self.sendTclient(csocket, command)
            using = self.recvFclient(csocket)
            self.writeTGui(Ip + ":Usage:" + using)
            print using
        if command == "Get Process List":
            self.sendTclient(csocket, command)
            lengf = self.recvFclient(csocket)
            self.writeTGui(Ip + ":"+lengf)
            x = int(lengf)
            for i in range(x):
                print str(i)
                PId = self.recvFclient(csocket)
                Namee = self.recvFclient(csocket)
                Usingg = self.recvFclient(csocket)
                s = Ip+":"+PId+";"+Namee+";"+Usingg
                self.writeTGui(s)
                print (s)
                    # self.dbconn.execute(
                #     """INSERT INTO Process_Table(PID ,Pname,Using) VALUES ('%s', '%s', '%s')""" % (PId, Namee, Usingg))
                # self.dbconn.commit()
        if command == "Get Open Windows":
            self.sendTclient(csocket,command)
            lens = self.recvFclient(csocket)
            self.writeTGui(Ip+":"+lens)
            leni = int(lens)
            for i in range(leni):
                self.writeTGui(Ip + ":"+ self.recvFclient(csocket))

    def start(self,path):
        self.ConnectToGui()
        self.ServerSocket.bind((LOCAL_ADDRESS, PORT))
        self.ServerSocket.listen(5)
        while True:
            client_socket, client_address = self.ServerSocket.accept()
            cs = threading.Thread(target=self.SessionWithClient, args=(client_socket,))
            self.client_Ips[client_socket] = str(client_address[0])
            cs.start()
            time.sleep(7)
            if self.IF_CLIENT_NOT_CONNECTED:
                self.IF_CLIENT_NOT_CONNECTED = False
                c = threading.Thread(target=self.Continues, args=(path,))
                c.start()

    def ConnectToGui(self):
        self.GUISocket.connect((GUI_ADDRESS, GUI_PORT))
        print self.GUISocket.recv(2048)


def main(argv):
    time.sleep(1)
    ser = server()
    ser.start(argv)


if __name__ == "__main__":
    main(sys.argv[1])
