## MainViewModel

For PTR Viewer Manual go [here](PTR_Viewer_Manual.md).

A data hub for a [MainWindow](PTR_Viewer_MainWindow.md). It contains all data and code for MainWindow to present. It also is concerned to populate Model classes with necessary [commands](#commands) to be used for buttons.

### Constructor

In [App.xaml.cs][1] MainViewModel is constructed with Execution parameter *true*, it is meant to distinguish a working MainViewModel from a test MainViewModel, which is instantiated by a Designer. So the constructor is devided in 3 parts:

* **Working Section** populates some lists with database queries.  
* **Test Section** populates same lists with test data.  
* **Common Section** sets a default View (Views are discussed in [MainWindow][2]) and other important parameters with current values, be it working data or test data.



### Properties

***ViewVisibility** Properties are mentioned in [MainWindow][2v].



#### Commands

>	public ICommand CommandName { get; private set; }  

A command property. A button is bound to that. It won't work itself, since it's just an interface.

>	private void SetupMainViewModelCommands() {  
		//SetupMergeViewCommands();  
		comToggleReports = new UVMCommand((p) => {  
		ReportsVisibility = (ReportsVisibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;  
		});  
		...

Here commands get their jobs. [UVMCommand][3] has some nifty things it can do. It has a name, a static CanExecute and a dynamic CanExecute, to suit any need. Moreover it can be a static parameter on a class defined in a different assembly. For examples, go to [UVMCommand.cs][3] file.



[1]:App.xaml.cs
[2]:PTR_Viewer_MainWindow.md
[2v]:PTR_Viewer_MainWindow.md#views
[3]:..//SharedLib/PTRModelClasses/UVMCommand.cs