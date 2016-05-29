"""
##################################################################
# Created By:  Hadar Rabin                                       #
# Date: 20/05/2016                                               #
# Name: Configuration connector to python scripts                #
# Version: 1.0                                                   #
# Windows Tested Versions: Win 7 64-bit                          #
# Python Tested Versions: 2.6 32-bit                             #
# Python Environment  : PyCharm                                  #
##################################################################
"""
from ConfigParser import SafeConfigParser

parser = SafeConfigParser()
parser.read('config.ini')

IP = str(parser.get('DEFAULT', 'IP'))

SIZE = int(parser.get('System', 'UNITS_SIZE'))

CPU_LIMIT = int(parser.get('System', 'CPU_LIMIT'))


