using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace neonrpg.Utilities {

    class Win32 {

        public const Int32 STD_INPUT_HANDLE = -10;
        public const Int32 STD_OUTPUT_HANDLE = -11;

        public const Int32 ENABLE_MOUSE_INPUT = 0x0010;
        public const Int32 ENABLE_QUICK_EDIT_MODE = 0x0040;
        public const Int32 ENABLE_EXTENDED_FLAGS = 0x0080;
        public const Int32 ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;
        public const Int32 ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        public const Int32 KEY_EVENT = 1;
        public const Int32 MOUSE_EVENT = 2;

        [DebuggerDisplay("EventType: {EventType}")]
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT_RECORD {
            [FieldOffset(0)]
            public Int16 EventType;
            [FieldOffset(4)]
            public KEY_EVENT_RECORD KeyEvent;
            [FieldOffset(4)]
            public MOUSE_EVENT_RECORD MouseEvent;
        }

        [DebuggerDisplay("{dwMousePosition.X}, {dwMousePosition.Y}")]
        public struct MOUSE_EVENT_RECORD {
            public COORD dwMousePosition;
            public Int32 dwButtonState;
            public Int32 dwControlKeyState;
            public Int32 dwEventFlags;
        }

        [DebuggerDisplay("KeyCode: {wVirtualKeyCode}")]
        [StructLayout(LayoutKind.Explicit)]
        public struct KEY_EVENT_RECORD {
            [FieldOffset(0)]
            [MarshalAsAttribute(UnmanagedType.Bool)]
            public Boolean bKeyDown;
            [FieldOffset(4)]
            public UInt16 wRepeatCount;
            [FieldOffset(6)]
            public UInt16 wVirtualKeyCode;
            [FieldOffset(8)]
            public UInt16 wVirtualScanCode;
            [FieldOffset(10)]
            public Char UnicodeChar;
            [FieldOffset(10)]
            public Byte AsciiChar;
            [FieldOffset(12)]
            public Int32 dwControlKeyState;
        };

        [DllImportAttribute("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern Boolean GetConsoleMode(IntPtr hConsoleHandle, ref Int32 lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImportAttribute("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern Boolean ReadConsoleInput(IntPtr hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

        [DllImportAttribute("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern Boolean SetConsoleMode(IntPtr hConsoleHandle, Int32 dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        //[SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);


        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteConsoleOutput(
          IntPtr hConsoleOutput,
          CHAR_INFO[] lpBuffer,
          COORD dwBufferSize,
          COORD dwBufferCoord,
          ref SMALL_RECT lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD {
            public short X;
            public short Y;

            public COORD(short X, short Y) {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_UNION {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_INFO {
            [FieldOffset(0)]
            public CHAR_UNION Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SMALL_RECT {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FillConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            char cCharacter,
            uint nLength,
            COORD dwWriteCoord,
            out uint lpNumberOfCharsWritten
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadConsoleOutput(
            IntPtr hConsoleOutput,
            [Out] CHAR_INFO[] lpBuffer,
            COORD dwBufferSize,
            COORD dwBufferCoord,
            ref SMALL_RECT lpReadRegion
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsole(
            IntPtr hConsoleOutput,
            string lpBuffer,
            uint nNumberOfCharsToWrite,
            out uint lpNumberOfCharsWritten,
            IntPtr lpReserved
        );
    }
}
