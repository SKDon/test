﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using log4net.Config;

[assembly: AssemblyTitle("Alicargo")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Alicargo")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("3c578bba-3704-41a1-bc8d-17233326c35e")]

[assembly: XmlConfigurator(Watch = true)]
[assembly: InternalsVisibleTo("Alicargo.Tests")]
[assembly: InternalsVisibleTo("Alicargo.BlackBox.Tests")]
[assembly: InternalsVisibleTo("Alicargo.TestHelpers")]
[assembly: InternalsVisibleTo("Alicargo.Jobs.BlackBox.Tests")]