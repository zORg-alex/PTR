using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using PTR.PTRLib;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using WinInterop = System.Windows.Interop;
using System;

namespace PTR.Viewer {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ModernWindow {

		//private MainViewModel mvm;
		//public MainViewModel Mvm {
		//	get { return mvm; }
		//	set { mvm = value; }
		//}


		public MainWindow() {
			InitializeComponent();

            //this.Loaded += new RoutedEventHandler(win_Loaded);
            this.SourceInitialized += new EventHandler(win_SourceInitialized);

            //Applying previous session settings TODO do it through DP
            //int view = ViewerSettings.Default.WindowLastView;
            //mvm.CurrentView = (MainViewModel.Views)view;
            //         if (Mvm.CurrentView == MainViewModel.Views.FolderView) { Mvm.SelectedObject = Mvm.SelectedADFolder; }
            //         if (Mvm.CurrentView == MainViewModel.Views.FunctionView) { Mvm.SelectedObject = Mvm.SelectedIFunction; }
            //if (Mvm.CurrentView == MainViewModel.Views.UserView) { Mvm.SelectedObject = Mvm.SelectedUUser; } //Not necessary

            double w = ViewerSettings.Default.WindowStartWidth;
			double h = ViewerSettings.Default.WindowStartHeight;
			double x = ViewerSettings.Default.WindowStartX;
			double y = ViewerSettings.Default.WindowStartY;

			//Checking if a window is out of bounds. May be a second monitor was expropriated from a user, or else ;P
			bool outOfBounds =
					(x <= SystemParameters.VirtualScreenLeft - w) ||
					(y <= SystemParameters.VirtualScreenTop - h) ||
					(SystemParameters.VirtualScreenLeft +
					SystemParameters.VirtualScreenWidth <= x) ||
					(SystemParameters.VirtualScreenTop +
					SystemParameters.VirtualScreenHeight <= y);

			//Setting default values for a first start
			if (w == 0 && h == 0 || outOfBounds) {
				if (!outOfBounds) {
					w = Width;
					h = Height;
				}
				x = (SystemParameters.FullPrimaryScreenWidth - w) / 2;
				y = (SystemParameters.FullPrimaryScreenHeight - h) / 2;
			}
			
			Width = w;
			Height = h;
			Left = x;
			Top = y;
		}

		protected override void OnClosing(CancelEventArgs e) {
			//Save settings on closing window
			ViewerSettings.Default.WindowStartWidth = Width;
			ViewerSettings.Default.WindowStartHeight = Height;
			ViewerSettings.Default.WindowStartX = Left;
			ViewerSettings.Default.WindowStartY = Top;
			//if (mvm !=null) ViewerSettings.Default.WindowLastView =	(int)mvm.CurrentView; TODO
			ViewerSettings.Default.Save();
			base.OnClosing(e);
		}

        //private void IFunctionListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
        //    if ((sender as ListBox).SelectedItem as FrameworkElement != null) ((sender as ListBox).SelectedItem as FrameworkElement).BringIntoView();
        //}


        void win_SourceInitialized(object sender, EventArgs e) {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(WindowProc));
        }

        private static System.IntPtr WindowProc(System.IntPtr hwnd, int msg, System.IntPtr wParam, System.IntPtr lParam, ref bool handled) {
            switch (msg) {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (System.IntPtr)0;
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam) {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            System.IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero) {

                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }


        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        //void win_Loaded(object sender, RoutedEventArgs e) {
        //    this.WindowState = WindowState.Maximized;
        //}


        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>            
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>            
            public int dwFlags = 0;
        }


        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT {
            /// <summary> Win32 </summary>
            public int left;
            /// <summary> Win32 </summary>
            public int top;
            /// <summary> Win32 </summary>
            public int right;
            /// <summary> Win32 </summary>
            public int bottom;

            /// <summary> Win32 </summary>
            public static readonly RECT Empty = new RECT();

            /// <summary> Win32 </summary>
            public int Width {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }
            /// <summary> Win32 </summary>
            public int Height {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public RECT(int left, int top, int right, int bottom) {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            /// <summary> Win32 </summary>
            public RECT(RECT rcSrc) {
                this.left = rcSrc.left;
                this.top = rcSrc.top;
                this.right = rcSrc.right;
                this.bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty {
                get {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }
            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString() {
                if (this == RECT.Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
            public override bool Equals(object obj) {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode() {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }


            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2) {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2) {
                return !(rect1 == rect2);
            }


        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
    }
}
