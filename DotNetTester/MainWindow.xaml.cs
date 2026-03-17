using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotNetTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        // setlocale category constants
        private const int LC_ALL = 0;
        private const int LC_COLLATE = 1;
        private const int LC_CTYPE = 2;
        private const int LC_MONETARY = 3;
        private const int LC_NUMERIC = 4;
        private const int LC_TIME = 5;

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr setlocale(int category, [MarshalAs(UnmanagedType.LPStr)] string? locale);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void _tzset();

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern long _time64(IntPtr destTime);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr _localtime64(ref long time);

        [StructLayout(LayoutKind.Sequential)]
        private struct CrtTm
        {
            public int tm_sec;   // seconds after the minute - [0, 60]
            public int tm_min;   // minutes after the hour - [0, 59]
            public int tm_hour;  // hours since midnight - [0, 23]
            public int tm_mday;  // day of the month - [1, 31]
            public int tm_mon;   // months since January - [0, 11]
            public int tm_year;  // years since 1900
            public int tm_wday;  // days since Sunday - [0, 6]
            public int tm_yday;  // days since January 1 - [0, 365]
            public int tm_isdst; // daylight saving time flag
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern nuint wcsftime
        (
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer,
            nuint sizeInWords,
            [MarshalAs(UnmanagedType.LPWStr)] string format,
            ref CrtTm tm
        );


        private void OnBtn001Clicked(object sender, RoutedEventArgs e)
        {
            TimeZoneInfo.ClearCachedData();     // CRT의 tzset와 같은 역할 - .NET의 Cached TimeZone을 초기화한다.
        }
        
        private void OnBtn002Clicked(object sender, RoutedEventArgs e)
        {
            DateTime dtnow = DateTime.Now;
            string localTime = dtnow.ToString("yyyy-MM-dd HH:mm:ss ");
            if(TimeZoneInfo.Local.IsDaylightSavingTime(dtnow))
            {
                MessageBox.Show(localTime + TimeZoneInfo.Local.DaylightName, "Local Time");
            }
            else
            {
                MessageBox.Show(localTime + TimeZoneInfo.Local.StandardName, "Local Time");
            }
        }
        
        private void OnBtn003Clicked(object sender, RoutedEventArgs e)
        {
            _tzset();   // CRT에 대해서만 동작함 - .NET의 Cached TimeZone에는 영향을 주지 않음
        }
        
        private void OnBtn004Clicked(object sender, RoutedEventArgs e)
        {
            long time = _time64(IntPtr.Zero);
            IntPtr tmPtr = _localtime64(ref time);
            if(tmPtr == IntPtr.Zero)
            {
                MessageBox.Show("_localtime64 failed.", "Error");
                return;
            }

            setlocale(LC_ALL, "");   // wcsftime의 표준시간대 StandardName이 깨지는 것을 방지하기 위하여 CRT에 CodePage를 CP_ACP로 설정

            CrtTm tm = Marshal.PtrToStructure<CrtTm>(tmPtr);

            var buffer = new StringBuilder(256);
            nuint result = wcsftime(buffer, 256, "%Y-%m-%d %H:%M:%S %Z", ref tm);
            if(result > 0)
            {
                string text = buffer.ToString(0, (int)result);
                MessageBox.Show(text, "wcsftime Result");
            }
            else
            {
                MessageBox.Show("wcsftime failed.", "Error");
            }
        }

        public IEnumerator TestCoroutine()
        {
            int i = 0;
            Console.WriteLine($"Coroutine {++i}");
            yield return null;

            Console.WriteLine($"Coroutine {++i}");
            yield return null;

            Console.WriteLine($"Coroutine {++i}");
            yield return null;
        }
       

        private void OnBtn005Clicked(object sender, RoutedEventArgs e)
        {
            AllocConsole(); // 콘솔 창 할당

            IEnumerator coroutine = TestCoroutine();

            Console.WriteLine("Main 1");
            coroutine.MoveNext();
            Console.WriteLine("Main 2");
            coroutine.MoveNext();
            Console.WriteLine("Main 3");
            coroutine.MoveNext();
        }

        private void OnBtn006Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn007Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn008Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn009Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn010Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn011Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn012Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void OnBtn013Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void OnBtn014Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn015Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn016Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn017Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn018Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn019Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn020Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn021Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn022Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn023Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn024Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn025Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn026Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn027Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn028Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn029Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn030Clicked(object sender, RoutedEventArgs e)
        {
        }
    }
}