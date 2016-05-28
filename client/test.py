import ctypes
EnumWindows = ctypes.windll.user32.EnumWindows
EnumWindowsProc = ctypes.WINFUNCTYPE(ctypes.c_bool, ctypes.POINTER(ctypes.c_int), ctypes.POINTER(ctypes.c_int))
GetWindowText = ctypes.windll.user32.GetWindowTextW
GetWindowTextLength = ctypes.windll.user32.GetWindowTextLengthW
IsWindowVisible = ctypes.windll.user32.IsWindowVisible
titles = []
def foreach_window(hwnd, lParam):
    if IsWindowVisible(hwnd):
        length = GetWindowTextLength(hwnd)
        buff = ctypes.create_unicode_buffer(length + 1)
        GetWindowText(hwnd, buff, length + 1)
        titles.append(buff.value)
        return True
EnumWindows(EnumWindowsProc(foreach_window), 0)
windows = {}
for i in range(len(titles)):
    if((titles[i] != None) and (len(titles[i]))>0):
        try:
            windows[i] = str(titles[i])
        except:
            pass
print str(len(windows))
for i in range(len(titles)):
    if((titles[i] != None) and (len(titles[i]))>0):
        try:
            print str(windows[i])
        except:
            pass
