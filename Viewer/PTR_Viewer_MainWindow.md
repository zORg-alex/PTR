## Main Window

For PTR Viewer Manual go [here](PTR_Viewer_Manual.md).

### Views

This is a Main window of a program. It was intended to make one  window for all the activity. It has few views, they are: 

* User View
* Merge View
* Function View
* Folder View

It was decided not to use tab control, since it constrains to its style, instead each view is a grid wich visibility is bound to [MainViewModel's][4] view visibility parameter (e.g. [UserViewVisibility][3]). Whole window uses one ViewModel, because many controls in different Views use same data and it is not effective to duplicate that functionality over many ViewModels. It gets messy when deprecated code is left there, so it is worth to wach over it and clean it and optimize it frequently. *First there was a try to use different ViewModels for each View, but it was not a good idea, because code got too fractured, hard to maintain.*

So, to change a View in a Designer change this line:

> [MainWindow.xaml][5]:23 <main:MainViewModel x:Key="TestViewModel" CurrentView="UserView" ReportsVisibility="Collapsed" MergeUUserFilter="a"/>

Setting CurrentView to any other, will be visible only in a Designer. The default UserView is set in a MainViewModel's [constructor][4con]. The program itself ignores it and instantiates a working ViewModel in [App.xaml.cs](App.xaml.cs)

### ModernUI Window

Because of numerous issues that rises when trying to create a custom styled window (setting WindowStyle to None and recreating it's usual behaviour) is too complicated and should have different solutions for differeent OSes and many solutions on the net are outdated and no longer working, it was decided to use an out of box solution. MahApp and MUI were studied. MahApp had a ready to modify clean window, but it had a heavy lag when resizing. The MUI was a Metro styled window with it's own way to organize things, it is light and easy to modify. It was decided to use MUI. The [StripoutModernUI.xaml][6] has a ModernWindow template that strips out all ModernUI styling and lets start from a clean and properly behaving window. It uses a previously created WindowButtons custom control with commands behaviour copied from MUI templates. Thus saving old window style.

Window position, size and view persistence between sessions is in [MainWindow.xaml.cs][7]

[1]:Resources/WhiteStyleRes.xaml 
[2]:Resources/Bump.png
[3]:MainViewModel.cs
[4]:PTR_Viewer_MainViewModel.md
[4con]:PTR_Viewer_MainViewModel.md#constructor
[5]:MainWindow.xaml
[6]:Resources/StripoutModernUI.xaml
[7]:MainWindow.xaml.cs