## ![Logo](..\Logo.png) PTR Viewer Manual

For PTR manual go [here](../README.md).


This is a **Windows Presentation Foundation** project. It connects to a 
PTR database and presents it's data to an administrator to overview
and export user data.

It is constructed on **MVVM pattern** (Model-ViewModel-View) 

* **Model** is a database and all classes used to model data.  
* **ViewModel** classes are called *ViewModel.cs to distinguish them. 
  This is what every Window and many controls get as DataContext.
* **Views** are all Windows and CustomControls that are named
  appropriately or stored in same named folder.

They communicate this way Vivew <--> ViewModel are interconnected, 
and ViewModel --> Model.

MVVM pattern helps to keep project flexible and easy to understand.
Since it is a big project it is better to keep it structured and 
junk free.  

> TODO create a WIP project and Clean project to wreck a
havok in WIP one and copy (once in a while) only necessary code to a 
Clean one. Will it work better? It helped a lot to rewrite whole 
project and strip out unnecessary and deprecated code once it was 
barely readable. BTW since then this stricture is as it is.

This project consists of:

* [MainWindow](PTR_Viewer_MainWindow.md)
* [ExportWindow]()
* [CustomControls]()
* [MainViewModel](PTR_Viewer_MainViewModel.md)
* [Exporter]()
