## ![Logo](Logo.png) PTR - Piekïûves Tiesîbas Registrs

This solution consists of few projects:
* [PTR.PTRLib]()  
	This library contains EF ModelFirst database and all it's partial
	classes. Plus some extra classes that are reused in this project.

* [Viewer](Viewer/PTR_Viewer_Manual.md)  
	This is a WPF project that renders the database to a visual
	representation.

* [ADT](PTR_ADT.md)  
	This is a ActiveDirectory parser and tracker service. It parses
	once and then tracks all folder accesses of any AD user.

Originally by _Alexey (**zORg_alex**) Ulianov_