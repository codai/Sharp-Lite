Welcome to S#arp Lite!

S#arp Lite is an architectural framework for the development of well-designed, custom-built, ASP.NET MVC applications.

For an introduction, see http://devlicio.us/blogs/billy_mccafferty/archive/2011/11/11/s-arp-lite-the-basicss.aspx

The code repository is found at https://github.com/codai/Sharp-Lite

If you'd like to see a couple of example projects, take a look in the \Example folder.

-----------------------------------

To get started with your first S#arp Lite project...

#) Install Visual Studio 2010

#) Install ASP.NET MVC 3

#) Install Templify, available from https://github.com/endjin/Templify/downloads (Templify-v0.6.15144.msi was used with this release.)

#) Copy \SharpLite\Template\s#arp-lite-project-v0.42.01.pkg to C:\Users\%USER%\AppData\Roaming\Endjin\Templify\repo\
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
