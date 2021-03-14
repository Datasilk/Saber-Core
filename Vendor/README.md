# Saber Vendor
Build vendor plugins for [Saber](https://saber.datasilk.io)

## Development Instructions
* Clone one of [Saber's](https://saber.datasilk.io) vendor plugin GitHub repositories listed at the bottom of this page, such as the [PageList](https://github.com/Datasilk/Saber-PageList) plugin
* Run command `npm install`
* **Build & test your vendor plugin in Visual Studio**:
  * Move your vendor project into the `/App/Vendors` folder within [Saber's](https://saber.datasilk.io) Visual Studio project
  * Run command `gulp vendors`
  * Run Saber in Visual Studio or via the command `dotnet run --project App` from Saber's project folder and navigate to your Saber website within your web browser ([http://localhost:7070](http://localhost:7070))
* **Build & test your vendor plugin release**:
  * Follow the [Publishing Instructions](#Publishing%20Instructions) to generate a **.7z** file
  * Copy the contents of `/bin/Publish/[win-x64 or linux-x64]/` into one of the following folders:
    *  the `/App/Vendors` folder within [Saber's](https://saber.datasilk.io) Visual Studio project 
    *  the `/Vendors` folder within [Saber's](https://saber.datasilk.io) published release folder
  *  Restart your web server or run the command `dotnet Saber.dll` from Saber's published release folder and navigate to your Saber website within your web browser

> Installed vendor plugins can be found by navigating to **File** > **Website Settings** within the Saber Editor. Some vendor plugins may not be visible from the Website Settings tab and would be considered "pass-through", meaning that the plugin "just works". 

> Vendor plugins cannot be disabled through the Saber Editor and so you must physically remove the vendor files from the Saber's `Vendors` folder to disable their functionality.

## Publishing Instructions
* Open `gulpfile.js` 
  * Change the value of the variable `var app = ''` to the name of your plugin (with no spaces or special characters)
  * look for the line `//include custom resources` inside the function `publishToPlatform(platform)` to optionally include any files that you wish to publish along with your plugin's release files. 
    > NOTE: There may already be custom resources included based on the GitHub repository that you cloned as a starting point for your own plugin. You should delete any of these custom resources that are unneccessary from your hard drive & from the gulpfile.
* Run command `./publish.bat` to build & package your plugin's release files
* Upload the **.7z** file generated under `/bin/Publish` to a new GitHub release within your GitHub repository. Alternatively, you can upload the **.7z** file to your own web server for distribution.
* Contact Datasilk at [support@datasilk.io](mailto:support@datasilk.io) with a link to the web page that is hosting your latest release download (for example, your GitHub repository), and we will consider adding your Saber vendor plugin to our **Supported Vendor Plugins** list in both this README file and on our official website ([saber.datasilk.io/plugins.html](https://saber.datasilk.io/plugins.html)).

## Vendor-Specific Functionality
* [IVendorStartup](#IVendorStartup)
* [IVendorViewRenderer](#IVendorViewRenderer)
* [IVendorController](#IVendorController)
* [IVendorKeys](#IVendorKeys)
* [IVendorHtmlComponents](#IVendorHtmlComponents)
* [IVendorContentField](#IVendorContentField)
* [IVendorWebsiteSettings](#IVendorWebsiteSettings)
* [IVendorEmailClient](#IVendorEmailClient)
* [IVendorEmails](#IVendorEmails)

#### IVendorStartup
Interface used to execute vendor-specific code when the Saber application starts up. All Vendor classes that inherit `IVendorStartup` will be evaluated via
Saber's `ConfigureServices` method and `Configure` method located in the `/App/Startup.cs` class.

``` csharp
public class Startup : IVendorStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //do stuff
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfigurationRoot config)
    {
        //do stuff
    }
}
```

#### IVendorViewRenderer
Interface used to execute vendor-specific code when Saber renders a View. Attribute `[ViewPath("/Views/Path/To/myfile.html")]` is required on the class that inherits `IVendorViewRenderer`, which will determine when the `Render` method is being called to load the associated `html` file. Use this interface to add HTML to a View that contains the `{{vendor}}` element.

``` csharp
[ViewPath("/Views/AppSettings/appsettings.html")]
public class MyPlugin : IVendorViewRenderer
{
    public void Render(Core.IRequest request, View view)
    {
        var myview = new View("/Vendor/MyPlugin/settings.html");
        view["vendor"] += myview.Render();
    }
}

```
In the example above, we append the rendered HTML of our `settings.html` view to the `vendor` element whenever Saber renders the `/Views/AppSettings/appsettings.html` View.
> **NOTE:** It is important that you append the rendered HTML to the contents of the `vendor` element instead of replacing the contents because other vendors might have appended content to the same element beforehand.

Saber supports the `IVendorViewRenderer` for all views within the application, and the following views include a `{{vendor}}` HTML variable so that vendors can extend the Editor UI.

* `/Views/AppSettings/appsettings.html`, used to add vendor-speicific Application Settings to Saber
* `/Views/PageSettings/pagesettings.html`, used to add vendor-speicific Page Settings to Saber

#### IVendorController
Interface used to route page requests to vendor-specific controllers. Your class must inherit `Controller` as well as `IVendorController` in order to work properly.
> **NOTE:** Make sure your controller names do not conflict with potential web pages that users will want to create for their website, such as:
>  `About`, `Contact`, `Blog`, `Wiki`, `Projects`, `Team`, `Terms`, `PrivacyPolicy`, `Members`, `Landing`, `Store`, `History`, etc.

``` csharp
public class RSSReader : Controller, IVendorController
{
    public override string Render(string body = "")
    {
        if (!CheckSecurity("view-rss")) { return base.Render("Access Denied"); }
        var view = new View("/Vendors/RSS/reader.html");
        view["feeds"] = RSS.RenderFeeds();
        return view.Render();
    }
}
```

#### IVendorKeys
Interface used to define a list of security keys that can be assigned to users in order to gain access to restricted features.

``` csharp
public class SecurityKeys : IVendorKeys
{
    public string Vendor { get; set; } = "RSS Feed Reader";
    public SecurityKey[] Keys { get; set; } = new SecurityKey[]
    {
        new SecurityKey(){Value = "manage-rss", Label = "Manage RSS Feeds", Description = "Add & Remove RSS feeds to read"},
        new SecurityKey(){Value = "view-rss", Label = "View RSS Feeds", Description = "Read articles from your feed reader"}
    };
}
```

> NOTE: The website administrator (UserId:1) will automatically have access to all security keys, and all other users will have to be given permission to have access to specific security keys.

#### IVendorHtmlComponents
An interface used to define a list of mustache variables with properties that are dynamically rendered to HTML. For example, check out the [Page List](https://github.com/Datasilk/Saber-PageList) plugin which utilizes the `IVendorHtmlComponents` interface to read mustache code such as `{{page-list path:"blog", length:"4"}}` to render a list of pages that exist in the user's website.

``` csharp
public class HtmlComponent : IVendorHtmlComponents
{
    public List<HtmlComponentModel> Bind()
    {
        //add custom html variable
        return new List<HtmlComponentModel>{
            new HtmlComponentModel() {
                Key = "photo-gallery",
                Name = "Photo Gallery",
                Description = "Display a photo gallery in various ways",
                Icon = "/Vendors/PhotoGallery/icon.svg",
                Parameters = new Dictionary<string, HtmlComponentParameter>()
                {
                    {
                        "length",
                        new HtmlComponentParameter()
                        {
                            Name = "Length",
                            DefaultValue = "",
                            Description = "Total thumbnail photos to display per page (if layout supports paging)."
                        }
                    },
                    {
                        "path",
                        new HtmlComponentParameter()
                        {
                            Name = "Path",
                            DefaultValue = "",
                            Description = "Relative path used to find the layout folder. Default is \"/Vendors/PhotoGallery/layouts/\"."
                        }
                    },
                    {
                        "layout",
                        new HtmlComponentParameter()
                        {
                            Name = "Layout",
                            DefaultValue = "",
                            Description = "Name of the layout to use. Default is \"grid\"."
                        }
                    }
                },
                Render = new Func<View, IRequest, Dictionary<string, string>, string, string, string, List<KeyValuePair<string, string>>>((view, request, args, data, prefix, key) =>
                {
                    var results = new List<KeyValuePair<string, string>>();
                    var path = args.ContainsKey("path") ? args["path"] : "/Vendors/PhotoGallery/layouts/";
                    var layout = args.ContainsKey("layout") ? args["layout"] : "grid";
                    var container = new View(path + layout + "/container.html");
                    var viewThumb = new View(path + layout + "/thumbnail.html");
                    var html = new StringBuilder();
                    if(data != "")
                    {
                        //render photo thumbnails...
                    }
                    results.Add(new KeyValuePair<string, string>(prefix + key, container.Render()));
                    return results;
                })
            }
        };
    }
}
```

##### Special Variables
You can also create HTML Components that are used as **Special Variables**, which are mustache variables that have no parameters.
A special variable can either be replaced with rendered HTML or if you include the property `Block = true` in your `HtmlComponentModel`, Saber will display a block of HTML that you provide when the special variable returns true. For example, using the special variable `{{user}}<div>My content here</div>{{/user}}` will show `<div>My content here</div>` if the user is logged into their account.

#### IVendorContentField
An interface used to render content fields within Saber's Page Content tab. If you are developing a custom mustache variable using `IViewDataBinder` (*as described above*), then you can utilize the `IVendorContentField` interface to provide a custom content field to manage the user's content related to your custom mustache variable. For example, a photo gallery (e.g. `{{photo-gallery length="10"}}`) would need a custom content field that allowed the user to manage (upload, sort, & remove) photos for their gallery.

``` csharp
[ContentField("photo-gallery")]
public class ContentField : IVendorContentField
{
    public string Render(IRequest request, Dictionary<string, string> args, string data, string id, string prefix, string key)
    {
        var view = new View("/Vendors/PhotoGallery/contentfield.html");
        var viewThumb = new View("/Vendors/PhotoGallery/thumbnail.html");
        if (!string.IsNullOrEmpty(data))
        {
            var html = new StringBuilder();
            var photos = JsonSerializer.Deserialize<List<Photo>>(data);
            foreach(var photo in photos)
            {
                viewThumb["thumbnail"] = photo.Thumbnail;
                html.Append(viewThumb.Render());
                viewThumb.Clear();
            }
            view["thumbnails"] = html.ToString();
        }
        request.AddCSS("/editor/vendors/photogallery/photogallery.css", "css-photogallery");
        request.AddScript("/editor/vendors/photogallery/photogallery.js", "js-photogallery", "() => {S.vendor.photogallery.load();}");
        return view.Render();
    }
}
```

#### IVendorWebsiteSettings
An interface used to add an accordion to Saber's website settings tab. The accordion is used to display website-related settings for your plugin.

``` csharp
public class WebsiteSettings : IVendorWebsiteSettings
{
    public string Name { get; set; } = "Import/Export Website";

    public string Render(IRequest request)
    {
        var html = new StringBuilder();
        var access = false;
        if (request.CheckSecurity("import")) {
            html.Append(Cache.LoadFile(App.MapPath("/Vendors/ImportExport/import.html")));
            access = true;
        }
        if (request.CheckSecurity("export"))
        {
            html.Append(Cache.LoadFile(App.MapPath("/Vendors/ImportExport/export.html")));
            access = true;
        }
        if (access)
        {
            request.AddScript("/editor/vendors/importexport/importexport.js");
        }
        return html.ToString();
    }
}
```

#### IVendorEmailClient
An interface used to define an email client and its parameters in order to connect to a remote email service and send emails to website users. Saber comes with an SMTP email client by default, so this interface would be used to connect to other services such as SendGrid or MailChimp.

``` csharp
public class MyEmailClient : IVendorEmailClient
{
    public string Id { get; set; } = "my-client";
    public string Name { get; set; } = "My Client";
    public Dictionary<string, EmailClientParameter> Parameters { get; set; } = new Dictionary<string, EmailClientParameter>()
    {
        {"service",
            new EmailClientParameter()
            {
                DataType = EmailClientDataType.List,
                Name = "Service",
                Description = "",
                ListOptions = new string[]
                {
                    "Gmail", "Hotmail", "Yahoo", "ProtonMail"
                }
            }
        },
        {"use-ssl",
            new EmailClientParameter()
            {
                DataType = EmailClientDataType.Boolean,
                Name = "Use SSL",
                Description = ""
            }
        },
        {"username",
            new EmailClientParameter()
            {
                DataType = EmailClientDataType.UserOrEmail,
                Name = "Username",
                Description = ""
            }
        },
        {"password",
            new EmailClientParameter()
            {
                DataType = EmailClientDataType.Password,
                Name = "Password",
                Description = ""
            }
        },
    };

    public Dictionary<string, string> GetConfig()
    {
        //get parameters from JSON config file
    }

    public void Init() { }

    public void SaveConfig(Dictionary<string, string> parameters)
    {
        //save parameters to JSON config file
    }

    public void Send(MailMessage message, string RFC2822_formatted)
    {
        //connect to service and send email
    }
}
```

#### IVendorEmails
An interface used to define a list of email actions. Email actions can be customized by website administrators by selecting which email client to use when sending an email for a specific action (such as the `signup` or `forgotpass` actions), and if allowed, they can also supply a user-defined subject line for the email. This interface should be used if your plugin needs the ability to send emails to users.

``` csharp
public class MyEmails : IVendorEmails
{
    public EmailType[] Types { get; set; } = new EmailType[]
    {
        new EmailType()
        {
            Key = "signup",
            Name = "Sign Up",
            Description = "An email sent when a new user signs up for Saber",
            TemplateFile = "signup.html",
            UserDefinedSubject = true
        },
        new EmailType()
        {
            Key = "forgotpass",
            Name = "Forgot Password",
            Description = "An email sent when an existing user requests to reset their password",
            TemplateFile = "forgot-pass.html",
            UserDefinedSubject = true
        }
    };
}   
```

## Currently supported plugins

#### CORS
* [Github repository](https://github.com/Datasilk/Saber-CORS)

Adds CORS-related headers to the controller or service response for trusted cross-origin domains. 

#### Import Export
* [Github repository](https://github.com/Datasilk/Saber-ImportExport)

A vendor plugin for Saber that allows webmasters to backup & restore all web content for their Saber website using a simple zip file. This is useful for creating nightly backups and can also be used to publish pending changes from your local workstation to your live website.

#### Page List
* [Github repository](https://github.com/Datasilk/Saber-PageList)

Display a list of webpages associated with your website, such as blog posts or wiki pages. 

#### Photo Gallery
* [Github repository](https://github.com/Datasilk/Saber-PhotoGallery)

Display a list of photos in various ways, such as a grid or slideshow.

#### Reset Cache
* [Github repository](https://github.com/Datasilk/Saber-ResetCache)

A vendor plugin for Saber that allows users to manually reset the stored cache of objects related to pages within their website. This could be useful if your website isn't loading correctly.

#### Replace Template
* [Github repository](https://github.com/Datasilk/Saber-ReplaceTemplate)

A vendor plugin for Saber that allows users to replace the template website that is included with Saber with the currently published website. This is useful if you plan on distributing a copy of Saber with a custom template website preinstalled.