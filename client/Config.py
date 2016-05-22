from ConfigParser import SafeConfigParser

parser = SafeConfigParser()
parser.read('config.ini')

IP = str(parser.get('DEFAULT', 'IP'))

SIZE = int(parser.get('System', 'UNITS_SIZE'))

CPU_LIMIT = int(parser.get('System', 'CPU_LIMIT'))


