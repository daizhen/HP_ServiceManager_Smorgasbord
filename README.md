#HP Service Manager Smorgasbord
---

The word ***smorgasbord*** comes from book named "Introduction to Algorithms"
> We have designed this book to be both versatile and complete. You should find it
useful for a variety of courses, from an undergraduate course in data structures up
through a graduate course in algorithms. Because we have provided considerably
more material than can fit in a typical one-term course, you can consider this book
to be a “buffet” or “***smorgasbord***” from which you can pick and choose the material
that best supports the course...


I named this project "Service Manager Smorgasbord" to imply providing comprehensive tools related to HP Service Manager.

This project designed to be extensible. It provides a uniform framework to run every standalone tools. And each tool provided as a plugin. 

There are 3 components for this projects.

##1. Frame.
This is start-up of the project, providing a common message console which shows the messages as HP Service Manager did.

Plugins are configured in [MenuConfig.xml](SM.Smorgasbord.Frame/MenuConfig.xml "MenuConfig.xml"). 

##2. Communication module.
This module contains methods to communicate with Service Manager App Server. And this module is created purely by re-engineering. 

By calling the following methods, communication with Service Manager turns to be easy.

###1. lib/RunCode

By using this class, you can run the javascript code in Service Manager Server side and return the running result.   

Example to return an simple string.
```csharp
RunCode runCode = new RunCode();
JsonRaw rawResult = runCode.runCode.RunJSCode<JsonRaw>(dataBus, "return system.functions.operator();");
//Get the result from JsonRaw.Text.
string currentOperaotrName = rawResult.Text;

```

Example to Return Complex result: [SearchForm.cs](SM.Smorgasbord.SarchString/SearchForm.cs "SearchForm.cs")

```csharp

 		private void SearchString()
        {
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus();

            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\SearchString.js");
            string codeToRun =ConstructJSForTables() + " return GetAllString(\"" + textToSearch + "\");";
            Collection<SearchResult> results = codeRunner.Run<Collection<SearchResult>>(dataBus, codeToRun);

            Invoke(new UpdateUI(delegate()
            {
                ShowData(results);
            }));
            //return rawData;
        }
```
###2. lib/JSCodeRunner

This class extend class RunCode and add `Include` method to include dependent javascript files when run a function. 

###3. lib/ImportFile

This class is used to import unload file to Service Manager.

###4. lib/UnloadFile

This class is used to export unload objects.

###5. lib/UnloadScriptFile

This class used to export unload data. (not include the detail object content).


##3. Plugins
Refer to search string sample, [SearchString](SM.Smorgasbord.SarchString "SarchString")