Welcome to S#arp Lite!

Get started with your first S#arp Lite project...

#) Install Visual Studio 2010

#) Install ASP.NET MVC 3

#) Install Templify, available from https://github.com/endjin/Templify/downloads (Templify-v0.6.15144.msi was used with this release.)

#) Copy \SharpLite\Template\s#arp-lite-project-v0.42.pkg to C:\Users\%USER%\AppData\Roaming\Endjin\Templify\repo\
(Note that the AppData folder is hidden by default.)

#) In Windows Explorer, create a new, empty folder for your S#arp Lite project (e.g., "MyNewProject")

#) Right click the new folder and click "Templify Here"

#) In the Templify dialog:
	- Select the template "S#arp Lite Project - #"
	- Enter the name of your root namespace (which is probably the same name as the proejct; e.g., "MyNewProject")
	- Click "Deploy Template" (should take well less than a minute to run)
	- Exit Templify when it completes

#) Open the folder and then open the newly created solution

#) Create your database in SQL Server

#) Update the connection string in:
	- YourProject.Web/Web.config
	- YourProject.Tests/App.config

#) F5 to see the site; you can also run the unit YourProject.Tests in NUnit to verify everything is working

Now, start building out your domain model and run the unit test CanGenerateDatabaseSchema to do just that...it'll be in the "Text Output" tab in NUnit.  You can then run that SQL against your DB.

------------------------------------------------------

Get the example project up and running...

#) Open \SharpLite\Example\MyStore\MyStore.sln

#) Create a database called MyStore in SQL Server

#) Update the connection string in:
	- MyStore.Web/Web.config
	- MyStore.Tests/App.config

#) Run the unit-test generated SQL at \SharpLite\Example\MyStore\app\MyStore.DB\schema\UnitTestGeneratedSchema.sql against your DB

#) Build the solution and run all the unit tests in MyStore.Tests...they should all pass

#) F5





                                                                                                                                                      
                 `-://++ooooo++//:.`                                                                                                                  
             `:/ooooooooooooooooooooo/-`                                                                                                              
          `:+ooooooooooooooooooooooooooo+-`                                                                                                           
        `/oooooooooooooooooooooooooooooosso:`                                                                                                         
      `:oooooooooooooo/--`-:/+ooooooooooossso-                                                                                    +hh+  `yhh.   omm+  
     .+ooooooooooooo/`        `+oooooooooossss+`                                                                                  yMMh  -NMM/   dMMh  
    .oooooooooooooooo-.::.`/..-/oooooooooossssso`                                                                                 yMMh  -NMM/   dMMh  
   .oooooooooooooooo/:/. ` .-.`.-:+ooooooosssssso`                                                                                yMMmssyMMMhsssmMMh  
   +ooooooooooooooo//:.--..+o+/+++ooooooooossssss/                                            `                                   yMMMMMMMMMMMMMMMMy  
  -ooooooooooooooo++oo+-.``-:     `.:ooooosssssssy`                                      `-+ymN:                                  `::::::::::::::::`  
  /oooooooo+/-.             -+::-``.-ooooossssssyy:            .:+shmNNNy          ./s:  sMMMMy                                   -++/`               
  +oooooo+.          `--:--:-oooooooooooosssssssyy+        -ohNMNmdhydMy.        .NMMm` `mMMMNsydmh/`                             sMMm`               
  +ooooooo/:---///- ./ooooooooooooooooooosssssssyy+     -smhs+:.`    .:          sMMMyyhmMMMmys+/-`                               sMMN-............  
  /oooo+++++o+ooo/`-oooooooooooooooooooosssssssyyy/    sMo`               `:+sydNMMNhyodMMMm. `                        ``         sMMMMMMMMMMMMMMMMs  
  -oo/`       ``// ...``..-/ooo+::--://ooossssyyyy`   .mNNhhhhhhhhhyyo/.  .odmssMMM/ `/MMMMmmNms. ./syy+`     .yNs  `+dMM/  `.`   sMMMdddddddddhhhy/  
   os+.`        `-  ``     -o+.           `-:oyyy+      `:hmNNNNNNNMMMMM+    `-mMMNdNNMMMNo:.` `+dMh/yMm`.ydN+sMM: /myhMN-+dNMd   sMMm.              
   .ossoo/-/:/++++/:.`.:-  ``                 oys`                 `-dMMoshmNMMMMy/-:mMMM+   `oNMy..hMM/  -MModNNosy.`mMddo.+Md   sMMm.         .--  
    .osssoooooooooooo/           `:::-.`.`  .oys`                `./ydo. `:+-hMMy  `dMMMo   .dMM+.omMMm`  /MNd-`` ` `yMMh. :my.   `--.`        `mMMs  
     .ossssssoooooooo-      ```..`+sssssyyysyyo`   .+y-  `.-/+shmNms.       yMMy  `dMMMo    oMMNhs:/MMs  oMMm-    :dMMMMdhdmo     ````         `mMMy  
      `/sssssssssso+/+/   .+ssssssssssyyyyyyy:   `sMMhdmNMMMmhy+:.         sNdo` `yMMMs      -.    `++:  `+:      .+MMmo+:.                    .mMMy  
        `/sssssss/`     ./sssssssssyyyyyyyy/`   sNMNmhyo+:-`              `-`    yMMMs                            `hMm.           +mNNNNNNNNNNNNMMMh  
          `:osss-     +ssssssssyyyyyyyyyo:    `  `                              :s+-`                             oMM:            /mmmmmmmmmmmmmmmmo  
             .:.      /yyyyyyyyyyyyys+:`    .sd         .d:  `  `          `                                     .MNd`              `````` ````````   
                       /yyyyysso+/-`      `+/ss ..s.`/:`ds: +o oh`.+/ `/-`ho /::..-s..+/                          .`                                  
                                        `yyo/yh yho+m:-ohys:m.sd::No:od:-dy-/NoN.sdo+No:                                                              
                                       `+/   `:`+. /o/`: /::+.:+:-o/-++:`/+-:/./`+. .o/-                                                              
                                       -`                                                                                                             
