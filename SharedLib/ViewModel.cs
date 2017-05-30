using PTR.PTRLib;
using PTR.PTRLib.Common;
using System.Windows;
using System.Windows.Input;

namespace PTR.PTRLib {
	/// <summary>
	/// Deprecated since ModernUI has all necessary behaviour
	/// </summary>
	public class ViewModel : Notifiable {

		public ViewModel() {
            //WindowBorderNormalThickness = new Thickness(0);
			WindowState = WindowState.Normal;
			OldState = WindowState.Maximized;
			SetupViewModelCommands();
		}

        /// <summary>
        /// Used to remember to what state to restore the window
        /// </summary>
		private WindowState _oldState;
		public WindowState OldState {
			get { return _oldState; }
			set { _oldState = value;
				RaisePropertyChanged("WindowState");
			}
		}

        /// <summary>
        /// Tracks Windows State and sets proper Window border thickness for normal behaviour
        /// </summary>
		private WindowState _windowState;
		public WindowState WindowState {
			get { return _windowState; }
			set {
				OldState = _windowState;
				_windowState = value;
				SetWindowBorderThickness();
				RaisePropertyChanged("WindowState");
			}
		}

        /// <summary>
        /// Minimizes or restores a window depending on a previous state
        /// </summary>
		public void MinimizeToggle(object sender, ExecutedRoutedEventArgs e) {
			if (OldState == WindowState.Normal) {
				if (WindowState == WindowState.Minimized) WindowState = WindowState.Normal;
				else WindowState = WindowState.Minimized;
			}
			else {
				if (WindowState == WindowState.Minimized) WindowState = WindowState.Maximized;
				else WindowState = WindowState.Minimized;
			}
		}

        /// <summary>
        /// Toggles between Maximized and Normal states
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public void MaximizeToggle(object sender, ExecutedRoutedEventArgs e) {
			if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
			else WindowState = WindowState.Maximized;
		}


		public ICommand comClose { get; private set; }
		public ICommand comMinimizeToggle { get; private set; }
		public ICommand comMaximizeToggle { get; private set; }

		private void SetupViewModelCommands() {
			comClose = new UVMCommand((p) => {
				Application.Current.Shutdown();
			},
			(p) => {
				return true;
			});

			comMaximizeToggle = new UVMCommand((p) => {
				if (WindowState == WindowState.Maximized) WindowState = OldState;
				else WindowState = WindowState.Maximized;
			},
			(p) => {
				return true;
			});

			comMinimizeToggle = new UVMCommand((p) => {
				if (OldState == WindowState.Normal) {
					if (WindowState == WindowState.Minimized) WindowState = WindowState.Normal;
					else WindowState = WindowState.Minimized;
				}
				else {
					if (WindowState == WindowState.Minimized) WindowState = WindowState.Maximized;
					else WindowState = WindowState.Minimized;
				}
			},
			(p) => {
				return true;
			});
		}

        /// <summary>
        /// Since window with a WindowChrome is meant to have some pixels of decorative border around it,
        /// we need to set it to something when maximized, or it will be stretched out of the screen.
        /// </summary>
		public void SetWindowBorderThickness() {
            WindowBorderThickness = (WindowState == WindowState.Maximized) ?
                new Thickness(8) :
                //WindowBorderNormalThickness;
                new Thickness(0);
		}
		private Thickness _windowBorderThickness;
		public Thickness WindowBorderThickness { get { return _windowBorderThickness; } set { _windowBorderThickness = value; RaisePropertyChanged("WindowBorderThickness"); } }
    }
}