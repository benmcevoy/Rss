using System;
using System.Linq;
using Rss.Server.Models;
using System.Web.Http;
using Manager = Radio7.Portable.Rss;
using Feed = Rss.Server.Models.Feed;
using Radio7.Portable.Rss.Import;

namespace Rss.Server.Controllers.API
{
    public class OpmlController : ApiController
    {
        private readonly FeedsDbEntities _context;

        public OpmlController(FeedsDbEntities context)
        {
            _context = context;
        }

        [AcceptVerbs("GET")]
        public string Import()
        {
            var rootFolder = OpmlImporter.Import(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<opml version=""1.0"">
    <head>
        <title>Ben subscriptions in Google Reader</title>
    </head>
    <body>
        <outline text=""Beta Exam Announcements""
            title=""Beta Exam Announcements"" type=""rss""
            xmlUrl=""http://blogs.technet.com/b/betaexams/atom.aspx"" htmlUrl=""http://blogs.technet.com/b/betaexams/""/>
        <outline text=""bookmarks"" title=""bookmarks"" type=""rss""
            xmlUrl=""http://benmcevoy.com.au/bm/bm.ashx"" htmlUrl=""http://benmcevoy.com.au""/>
        <outline text=""Events organized by Victoria .NET Dev SIG""
            title=""Events organized by Victoria .NET Dev SIG"" type=""rss""
            xmlUrl=""https://www.eventbrite.com/rss/organizer_list_events/599681591"" htmlUrl=""http://www.eventbrite.com/org/599681591""/>
        <outline text=""Kiandra IT RSS Feed"" title=""Kiandra IT RSS Feed""
            type=""rss""
            xmlUrl=""http://kdev.com.au/createrssfeed.action?types=page&amp;pageSubTypes=comment&amp;pageSubTypes=attachment&amp;types=blogpost&amp;blogpostSubTypes=comment&amp;blogpostSubTypes=attachment&amp;types=mail&amp;spaces=conf_all&amp;title=Kiandra+IT+RSS+Feed&amp;labelString%3D&amp;excludedSpaceKeys%3D&amp;sort=modified&amp;maxResults=10&amp;timeSpan=5&amp;showContent=true&amp;showDiff=true&amp;confirm=Create+RSS+Feed&amp;os_authType=basic&amp;os_username=ben.mcevoy&amp;os_password=kss@kss24&amp;"" htmlUrl=""http://kdev.com.au""/>
        <outline text=""Melbourne Patterns Group""
            title=""Melbourne Patterns Group"" type=""rss""
            xmlUrl=""http://melbournepatterns.org/feed/"" htmlUrl=""http://melbournepatterns.org""/>
        <outline text=""Rosscott, Inc."" title=""Rosscott, Inc."" type=""rss""
            xmlUrl=""http://feeds.feedburner.com/systemcomic?format=xml"" htmlUrl=""http://www.systemcomic.com""/>
        <outline text=""Search results for 'beta'""
            title=""Search results for 'beta'"" type=""rss""
            xmlUrl=""http://borntolearn.mslearn.net/search/searchrss.aspx?q=beta&amp;sort=date%20desc"" htmlUrl=""http://borntolearn.mslearn.net/search/?q=beta&amp;sort=date%20desc""/>
        <outline text=""Site Root"" title=""Site Root"" type=""rss""
            xmlUrl=""http://borntolearn.mslearn.net/rss.aspx"" htmlUrl=""http://borntolearn.mslearn.net/default.aspx""/>
        <outline text=""The World of James"" title=""The World of James""
            type=""rss""
            xmlUrl=""http://theworldofjamesinmot.blogspot.com/feeds/posts/default"" htmlUrl=""http://theworldofjamesinmot.blogspot.com/""/>
        <outline text=""xkcd.com"" title=""xkcd.com"" type=""rss""
            xmlUrl=""http://xkcd.com/rss.xml"" htmlUrl=""http://xkcd.com/""/>
        <outline title=""dev"" text=""dev"">
            <outline text="".NET Ramblings - Brian Noyes' Blog""
                title="".NET Ramblings - Brian Noyes' Blog"" type=""rss""
                xmlUrl=""http://www.softinsight.com/bnoyes/SyndicationService.asmx/GetRss"" htmlUrl=""http://briannoyes.net/""/>
            <outline text=""10x Software Development""
                title=""10x Software Development"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/10xSoftwareDevelopment"" htmlUrl=""http://forums.construx.com/blogs/stevemcc/default.aspx""/>
            <outline text=""48KLoCs of Power"" title=""48KLoCs of Power""
                type=""rss""
                xmlUrl=""http://48klocs.blogspot.com/feeds/posts/default?alt=rss"" htmlUrl=""http://48klocs.blogspot.com/""/>
            <outline text=""4GuysFromRolla.com Headlines""
                title=""4GuysFromRolla.com Headlines"" type=""rss""
                xmlUrl=""http://aspnet.4guysfromrolla.com/rss/rss.aspx"" htmlUrl=""http://www.4GuysFromRolla.com""/>
            <outline text=""a geek trapped in a cool guy's body""
                title=""a geek trapped in a cool guy's body"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/jasonkemp"" htmlUrl=""http://www.ageektrapped.com/blog""/>
            <outline text=""A List Apart"" title=""A List Apart"" type=""rss""
                xmlUrl=""http://www.alistapart.com/rss.xml"" htmlUrl=""http://alistapart.com""/>
            <outline text=""Adam Nathan's Blog""
                title=""Adam Nathan's Blog"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/adam_nathan/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/adam_nathan/""/>
            <outline text=""Adventures with WPF : C#""
                title=""Adventures with WPF : C#"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/mgrayson/rss_tag_C_2300_.xml"" htmlUrl=""http://blogs.msdn.com/b/mgrayson/archive/tags/C_2300_/""/>
            <outline text=""All Your Base Are Belong To Us""
                title=""All Your Base Are Belong To Us"" type=""rss""
                xmlUrl=""http://blogs.microsoft.co.il/blogs/sasha/rss.aspx"" htmlUrl=""http://blogs.microsoft.co.il/blogs/sasha/""/>
            <outline text=""amazedsaint's .net journal""
                title=""amazedsaint's .net journal"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/amazedsaint/articles"" htmlUrl=""http://www.amazedsaint.com/""/>
            <outline text=""Andrew Smith"" title=""Andrew Smith"" type=""rss""
                xmlUrl=""http://agsmith.wordpress.com/feed/"" htmlUrl=""http://agsmith.wordpress.com""/>
            <outline text=""Ask Dr. WPF"" title=""Ask Dr. WPF"" type=""rss""
                xmlUrl=""http://www.drwpf.com/blog/Home/tabid/36/rssid/1/Default.aspx"" htmlUrl=""http://drwpf.com/blog""/>
            <outline text=""Ayende @ Rahien"" title=""Ayende @ Rahien""
                type=""rss""
                xmlUrl=""http://feeds.feedburner.com/AyendeRahien"" htmlUrl=""http://ayende.com/blog/""/>
            <outline text=""B# .NET Blog"" title=""B# .NET Blog"" type=""rss""
                xmlUrl=""http://community.bartdesmet.net/blogs/bart/rss.aspx"" htmlUrl=""http://community.bartdesmet.net/blogs/bart/default.aspx""/>
            <outline text=""Beatriz Costa"" title=""Beatriz Costa""
                type=""rss""
                xmlUrl=""http://www.beacosta.com/blog/?feed=atom"" htmlUrl=""http://bea.stollnitz.com/blog""/>
            <outline text=""Bill Blogs in C#"" title=""Bill Blogs in C#""
                type=""rss""
                xmlUrl=""http://feeds.feedburner.com/billwagner"" htmlUrl=""http://www.srtsolutions.com""/>
            <outline text=""Bite Size Standards""
                title=""Bite Size Standards"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/bitesizestandards"" htmlUrl=""http://bitesizestandards.com/""/>
            <outline text=""Björn Rochel's weblog""
                title=""Björn Rochel's weblog"" type=""rss""
                xmlUrl=""http://www.bjoernrochel.de/feed/"" htmlUrl=""http://www.bjoernrochel.de""/>
            <outline text=""Blair Davidson"" title=""Blair Davidson""
                type=""rss""
                xmlUrl=""http://bjdavidson.wordpress.com/feed/"" htmlUrl=""http://loosechainsaw.com/blog""/>
            <outline text=""Blog du Tristank"" title=""Blog du Tristank""
                type=""rss""
                xmlUrl=""http://blogs.technet.com/tristank/rss.xml"" htmlUrl=""http://blogs.technet.com/b/tristank/""/>
            <outline text=""Brad Abrams"" title=""Brad Abrams"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/brada/atom.xml"" htmlUrl=""http://blogs.msdn.com/b/brada/""/>
            <outline text=""Brownie Points"" title=""Brownie Points""
                type=""rss""
                xmlUrl=""http://blog.planetwpf.com/syndication.axd"" htmlUrl=""http://blog.planetwpf.com/""/>
            <outline text=""Bug Bash"" title=""Bug Bash"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/bugbash"" htmlUrl=""http://www.bugbash.net""/>
            <outline text=""C# Disciples"" title=""C# Disciples"" type=""rss""
                xmlUrl=""http://marlongrech.wordpress.com/feed/"" htmlUrl=""http://marlongrech.wordpress.com""/>
            <outline text=""C++ Soup!"" title=""C++ Soup!"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/CppSoup"" htmlUrl=""http://www.cplusplus-soup.com/""/>
            <outline text=""Charlie Calvert's Community Blog""
                title=""Charlie Calvert's Community Blog"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/charlie/atom.xml"" htmlUrl=""http://blogs.msdn.com/b/charlie/""/>
            <outline text=""CoDe Magazine News""
                title=""CoDe Magazine News"" type=""rss""
                xmlUrl=""http://www.code-magazine.com/CodeRSS.aspx"" htmlUrl=""http://www.code-magazine.com/""/>
            <outline
                text=""CodeBetter.Com - Stuff you need to Code Better!""
                title=""CodeBetter.Com - Stuff you need to Code Better!""
                type=""rss""
                xmlUrl=""http://feeds2.feedburner.com/codebetter"" htmlUrl=""http://codebetter.com""/>
            <outline text=""CSS Insider"" title=""CSS Insider"" type=""rss""
                xmlUrl=""http://css.weblogsinc.com/rss.xml"" htmlUrl=""http://css.weblogsinc.com""/>
            <outline
                text=""Daniel Cazzulino's Blog : Why XAML makes System.Configuration and ...""
                title=""Daniel Cazzulino's Blog : Why XAML makes System.Configuration and ...""
                type=""rss""
                xmlUrl=""http://www.clariusconsulting.net/blogs/kzu/rss.aspx"" htmlUrl=""http://www.clariusconsulting.net/blogs/kzu/default.aspx""/>
            <outline text=""Data See, Data Do"" title=""Data See, Data Do""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/mikehillberg/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/mikehillberg/""/>
            <outline text=""Dave Shea's mezzoblue""
                title=""Dave Shea's mezzoblue"" type=""rss""
                xmlUrl=""http://www.mezzoblue.com/rss/index.xml"" htmlUrl=""http://mezzoblue.com/""/>
            <outline text=""Dax Pandhi's Blog"" title=""Dax Pandhi's Blog""
                type=""rss""
                xmlUrl=""http://feeds.feedburner.com/daxpandhisblog"" htmlUrl=""http://www.nukeation.com/blog/""/>
            <outline
                text=""developer.* Blogs - Software Development Blogs""
                title=""developer.* Blogs - Software Development Blogs""
                type=""rss""
                xmlUrl=""http://www.developerdotstar.com/community/node/feed"" htmlUrl=""http://www.developerdotstar.com/community""/>
            <outline text=""Devlicio.us"" title=""Devlicio.us"" type=""rss""
                xmlUrl=""http://feeds2.feedburner.com/Devlicious"" htmlUrl=""http://devlicio.us/blogs/""/>
            <outline text=""Diego Vega"" title=""Diego Vega"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/diego/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/diego/""/>
            <outline text=""Entity Framework Design""
                title=""Entity Framework Design"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/efdesign/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/efdesign/""/>
            <outline text=""Fabulous Adventures In Coding""
                title=""Fabulous Adventures In Coding"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/ericlippert/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/ericlippert/""/>
            <outline text=""I know the answer (it's 42)""
                title=""I know the answer (it's 42)"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/abhinaba/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/abhinaba/""/>
            <outline text=""IanG on Tap"" title=""IanG on Tap"" type=""rss""
                xmlUrl=""http://www.interact-sw.co.uk/iangblog/rss2.0"" htmlUrl=""http://www.interact-sw.co.uk/iangblog/""/>
            <outline text=""ISerializable - Roy Osherove's Blog""
                title=""ISerializable - Roy Osherove's Blog"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/Iserializable"" htmlUrl=""http://osherove.com/blog/""/>
            <outline
                text=""Jaime Rodriguez : Windows Presentation Foundation""
                title=""Jaime Rodriguez : Windows Presentation Foundation""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/jaimer/rss_tag_Windows+Presentation+Foundation.xml"" htmlUrl=""http://blogs.msdn.com/b/jaimer/archive/tags/Windows+Presentation+Foundation/""/>
            <outline text=""Joel on Software"" title=""Joel on Software""
                type=""rss""
                xmlUrl=""http://www.joelonsoftware.com/rss.xml"" htmlUrl=""http://www.joelonsoftware.com""/>
            <outline text=""John Lam on Software""
                title=""John Lam on Software"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/LessIsBetter"" htmlUrl=""http://iunknown.com""/>
            <outline text=""Josh Smith on WPF"" title=""Josh Smith on WPF""
                type=""rss""
                xmlUrl=""http://joshsmithonwpf.wordpress.com/feed/"" htmlUrl=""http://joshsmithonwpf.wordpress.com""/>
            <outline text=""K. Scott Allen"" title=""K. Scott Allen""
                type=""rss""
                xmlUrl=""http://odetocode.com/Blogs/scott/atom.aspx"" htmlUrl=""http://www.google.com/reader/view/feed%2Fhttp%3A%2F%2Fodetocode.com%2FBlogs%2Fscott%2Fatom.aspx""/>
            <outline text=""Karen's Space"" title=""Karen's Space""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/karenliu/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/karenliu/""/>
            <outline text=""Karl On WPF - .Net""
                title=""Karl On WPF - .Net"" type=""rss""
                xmlUrl=""http://karlshifflett.wordpress.com/feed/"" htmlUrl=""http://karlshifflett.wordpress.com""/>
            <outline text=""Kent Boogaart"" title=""Kent Boogaart""
                type=""rss""
                xmlUrl=""http://kentb.blogspot.com/feeds/posts/default"" htmlUrl=""http://kentb.blogspot.com/""/>
            <outline text=""Kevin@Work"" title=""Kevin@Work"" type=""rss""
                xmlUrl=""http://work.j832.com/feeds/posts/default"" htmlUrl=""http://work.j832.com/""/>
            <outline text=""Kirill Osenkov"" title=""Kirill Osenkov""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/kirillosenkov/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/kirillosenkov/""/>
            <outline text=""Lester's WPF blog"" title=""Lester's WPF blog""
                type=""rss"" xmlUrl=""http://blogs.msdn.com/llobo/atom.xml"" htmlUrl=""http://blogs.msdn.com/b/llobo/""/>
            <outline text=""Mark Johnston's Developer Experiences : WPF""
                title=""Mark Johnston's Developer Experiences : WPF""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/markjo/rss_tag_WPF.xml"" htmlUrl=""http://blogs.msdn.com/b/markjo/archive/tags/WPF/""/>
            <outline text=""MarkItUp - Thinking Products...""
                title=""MarkItUp - Thinking Products..."" type=""rss""
                xmlUrl=""http://markitup.com/Rss.aspx"" htmlUrl=""http://markitup.com/""/>
            <outline text=""mob's dev blog"" title=""mob's dev blog""
                type=""rss""
                xmlUrl=""http://blog.mike-obrien.net/SyndicationService.asmx/GetRssCategory?categoryName=.NET"" htmlUrl=""http://blog.mikeobrien.net/""/>
            <outline
                text=""More thoughts on more thoughts on XAML, C# and WPF""
                title=""More thoughts on more thoughts on XAML, C# and WPF""
                type=""rss""
                xmlUrl=""http://pluralsight.com/blogs/dbox/rss.aspx"" htmlUrl=""http://www.pluralsight-training.net/community/blogs/dbox/default.aspx""/>
            <outline text=""OmegaMan's Musings""
                title=""OmegaMan's Musings"" type=""rss""
                xmlUrl=""http://www.omegacoder.com/?feed=atom"" htmlUrl=""http://omegacoder.com/""/>
            <outline text=""Omer van Kloeten's .NET Zen""
                title=""Omer van Kloeten's .NET Zen"" type=""rss""
                xmlUrl=""http://weblogs.asp.net/okloeten/rss.aspx"" htmlUrl=""http://weblogs.asp.net/okloeten/default.aspx""/>
            <outline text=""orktane"" title=""orktane"" type=""rss""
                xmlUrl=""http://www.orktane.com/Blog/syndication.axd?format=atom"" htmlUrl=""http://www.orktane.com/""/>
            <outline
                text=""Pablo Galiano : Deploying VSX Custom projects""
                title=""Pablo Galiano : Deploying VSX Custom projects""
                type=""rss""
                xmlUrl=""http://www.clariusconsulting.net/blogs/pga/rss.aspx"" htmlUrl=""http://www.clariusconsulting.net/blogs/pga/default.aspx""/>
            <outline text=""Paul Irish"" title=""Paul Irish"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/paul-irish"" htmlUrl=""http://paulirish.com/""/>
            <outline text=""Pete W's idea book""
                title=""Pete W's idea book"" type=""rss""
                xmlUrl=""http://www.acceptedeclectic.com/atom.xml"" htmlUrl=""http://www.acceptedeclectic.com/Blog.html""/>
            <outline text=""Rands In Repose"" title=""Rands In Repose""
                type=""rss""
                xmlUrl=""http://www.randsinrepose.com/index.xml"" htmlUrl=""http://www.randsinrepose.com/""/>
            <outline text=""Reflective Perspective""
                title=""Reflective Perspective"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/ReflectivePerspective"" htmlUrl=""http://blog.cwa.me.uk""/>
            <outline text=""Rob Relyea"" title=""Rob Relyea"" type=""rss""
                xmlUrl=""http://rrelyea.spaces.live.com/feed.rss"" htmlUrl=""http://robrelyea.wordpress.com""/>
            <outline text=""Rudi Grobler"" title=""Rudi Grobler"" type=""rss""
                xmlUrl=""http://dotnet.org.za/rudi/rss.aspx"" htmlUrl=""http://dotnet.org.za/rudi/default.aspx""/>
            <outline text=""sachabarber.net"" title=""sachabarber.net""
                type=""rss"" xmlUrl=""http://sachabarber.net/?feed=atom"" htmlUrl=""http://sachabarbs.wordpress.com/""/>
            <outline text=""ScottGu's Blog"" title=""ScottGu's Blog""
                type=""rss""
                xmlUrl=""http://weblogs.asp.net/scottgu/atom.aspx"" htmlUrl=""http://weblogs.asp.net/scottgu/default.aspx""/>
            <outline text=""Shannon Braun's Weblog""
                title=""Shannon Braun's Weblog"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/sysknowlogy"" htmlUrl=""http://www.sysknowlogy.com/""/>
            <outline text=""ShawnVN's Blog"" title=""ShawnVN's Blog""
                type=""rss""
                xmlUrl=""http://weblogs.asp.net/savanness/rss.aspx"" htmlUrl=""http://weblogs.asp.net/savanness/default.aspx""/>
            <outline text=""Sheva's TechSpace"" title=""Sheva's TechSpace""
                type=""rss""
                xmlUrl=""http://shevaspace.spaces.live.com/feed.rss"" htmlUrl=""http://shevaspace.spaces.live.com/""/>
            <outline text=""Stan Lippman's BLog""
                title=""Stan Lippman's BLog"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/slippman/atom.xml"" htmlUrl=""http://blogs.msdn.com/b/slippman/""/>
            <outline
                text=""Stefan Wick's Weblog - Development with Silverlight, WPF and Tablet PC""
                title=""Stefan Wick's Weblog - Development with Silverlight, WPF and Tablet PC""
                type=""rss"" xmlUrl=""http://blogs.msdn.com/swick/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/swick/""/>
            <outline text=""Steve Sanderson's blog""
                title=""Steve Sanderson's blog"" type=""rss""
                xmlUrl=""http://feeds.codeville.net/SteveCodeville"" htmlUrl=""http://blog.stevensanderson.com""/>
            <outline text=""The Bit Bucket"" title=""The Bit Bucket""
                type=""rss""
                xmlUrl=""http://msmvps.com/blogs/greglow/rss.aspx"" htmlUrl=""http://msmvps.com/blogs/greglow/default.aspx""/>
            <outline text=""The JavaScript Weblog""
                title=""The JavaScript Weblog"" type=""rss""
                xmlUrl=""http://javascript.weblogsinc.com/rss.xml"" htmlUrl=""http://javascript.weblogsinc.com""/>
            <outline text=""The Moth"" title=""The Moth"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/DanielMoth"" htmlUrl=""http://www.danielmoth.com/Blog/""/>
            <outline text=""The Visual Basic Team : Young Joo""
                title=""The Visual Basic Team : Young Joo"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/vbteam/rss_tag_Young+Joo.xml"" htmlUrl=""http://blogs.msdn.com/b/vbteam/archive/tags/Young+Joo/""/>
            <outline text=""the WPF way..."" title=""the WPF way...""
                type=""rss""
                xmlUrl=""http://pavanpodila.spaces.live.com/feed.rss"" htmlUrl=""http://pavanpodila.spaces.live.com/""/>
            <outline text=""theWPFblog"" title=""theWPFblog"" type=""rss""
                xmlUrl=""http://thewpfblog.com/?feed=rss2"" htmlUrl=""http://thewpfblog.com""/>
            <outline text=""Thoughts from the Wet Coast""
                title=""Thoughts from the Wet Coast"" type=""rss""
                xmlUrl=""http://www.charlesnurse.com/syndication.axd?format=rss"" htmlUrl=""http://www.charlesnurse.com/""/>
            <outline text=""Tim Sneath"" title=""Tim Sneath"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/tims/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/tims/""/>
            <outline text=""Togras Thoughts"" title=""Togras Thoughts""
                type=""rss""
                xmlUrl=""http://togras.blogspot.com/feeds/posts/default"" htmlUrl=""http://togras.blogspot.com/""/>
            <outline text=""unfold"" title=""unfold"" type=""rss""
                xmlUrl=""http://themechanicalbride.blogspot.com/feeds/posts/default?alt=rss"" htmlUrl=""http://themechanicalbride.blogspot.com/""/>
            <outline text=""Visual C++ Team Blog""
                title=""Visual C++ Team Blog"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/vcblog/atom.xml"" htmlUrl=""http://blogs.msdn.com/b/vcblog/""/>
            <outline text=""weirdlover"" title=""weirdlover"" type=""rss""
                xmlUrl=""http://www.weirdlover.com/feed/"" htmlUrl=""http://www.weirdlover.com""/>
            <outline text=""Windows Presentation Foundation SDK""
                title=""Windows Presentation Foundation SDK"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/wpfsdk/rss.xml"" htmlUrl=""http://blogs.msdn.com/b/wpfsdk/""/>
            <outline
                text=""Windows Presentation Foundation SDK : Styles and Templates""
                title=""Windows Presentation Foundation SDK : Styles and Templates""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/wpfsdk/rss_tag_Styles+and+Templates.xml"" htmlUrl=""http://blogs.msdn.com/b/wpfsdk/archive/tags/Styles+and+Templates/""/>
            <outline
                text=""WPF Team Bloggers : Windows Presentation Foundation""
                title=""WPF Team Bloggers : Windows Presentation Foundation""
                type=""rss""
                xmlUrl=""http://wpf.netfx3.com/blogs/presentation_bloggers/rss.aspx?Tags=Windows+Presentation+Foundation&amp;AndTags=1"" htmlUrl=""http://wpf.netfx3.com/blogs/presentation_bloggers/archive/tags/Windows+Presentation+Foundation/default.aspx""/>
            <outline text=""WPF Team Bloggers : WPF""
                title=""WPF Team Bloggers : WPF"" type=""rss""
                xmlUrl=""http://wpf.netfx3.com/blogs/presentation_bloggers/rss.aspx?Tags=WPF&amp;AndTags=1"" htmlUrl=""http://wpf.netfx3.com/blogs/presentation_bloggers/archive/tags/WPF/default.aspx""/>
            <outline text=""WPF Wonderland"" title=""WPF Wonderland""
                type=""rss""
                xmlUrl=""http://wpfwonderland.wordpress.com/feed/"" htmlUrl=""http://blog.wpfwonderland.com""/>
            <outline text=""XamlXaml.com"" title=""XamlXaml.com"" type=""rss""
                xmlUrl=""http://xamlxaml.com/feed/"" htmlUrl=""http://xamlxaml.com""/>
        </outline>
        <outline title=""food"" text=""food"">
            <outline text=""101 Cookbooks"" title=""101 Cookbooks""
                type=""rss""
                xmlUrl=""http://feeds.101cookbooks.com/101cookbooks"" htmlUrl=""http://www.101cookbooks.com/""/>
            <outline text=""Bong Mom's CookBook""
                title=""Bong Mom's CookBook"" type=""rss""
                xmlUrl=""http://www.bongcookbook.com/feeds/posts/default"" htmlUrl=""http://www.bongcookbook.com/""/>
            <outline text=""Footscray Food Blog""
                title=""Footscray Food Blog"" type=""rss""
                xmlUrl=""http://footscrayfoodblog.blogspot.com/feeds/posts/default"" htmlUrl=""http://footscrayfoodblog.blogspot.com/""/>
            <outline text=""Mahanandi"" title=""Mahanandi"" type=""rss""
                xmlUrl=""http://www.themahanandi.org/feed/"" htmlUrl=""http://www.themahanandi.org""/>
            <outline text=""melbourne gastronome""
                title=""melbourne gastronome"" type=""rss""
                xmlUrl=""http://www.melbournegastronome.com/feeds/posts/default"" htmlUrl=""http://www.melbournegastronome.com/""/>
        </outline>
        <outline title=""Locals"" text=""Locals"">
            <outline text=""Ben Mcevoy"" title=""Ben Mcevoy"" type=""rss""
                xmlUrl=""http://benmcevoy.com.au/blog/feed"" htmlUrl=""http://benmcevoy.com.au/blog/blog""/>
            <outline text=""Blair Davidson"" title=""Blair Davidson""
                type=""rss""
                xmlUrl=""http://bjdavidson.wordpress.com/feed/"" htmlUrl=""http://loosechainsaw.com/blog""/>
            <outline text=""Blog - Oscar Cao"" title=""Blog - Oscar Cao""
                type=""rss"" xmlUrl=""http://oscarcao.com/blog/feed"" htmlUrl=""http://oscarcao.com/blog/blog""/>
            <outline text=""David Nguyen"" title=""David Nguyen"" type=""rss""
                xmlUrl=""https://david844.wordpress.com/feed/"" htmlUrl=""https://david844.wordpress.com""/>
            <outline text=""Hendry Luk -- Sheep in Fence""
                title=""Hendry Luk -- Sheep in Fence"" type=""rss""
                xmlUrl=""http://hendryluk.wordpress.com/feed/"" htmlUrl=""http://hendryluk.wordpress.com""/>
            <outline text=""Kiandra IT Blog"" title=""Kiandra IT Blog""
                type=""rss"" xmlUrl=""http://blog.kiandra.com.au/feed/"" htmlUrl=""http://blog.kiandra.com.au""/>
            <outline text=""Matthew Sorvaag"" title=""Matthew Sorvaag""
                type=""rss"" xmlUrl=""http://matthew.sorvaag.net/feed/"" htmlUrl=""http://matthew.sorvaag.net""/>
            <outline text=""Nothing Insightful""
                title=""Nothing Insightful"" type=""rss""
                xmlUrl=""http://taitems.tumblr.com/rss"" htmlUrl=""http://taitems.tumblr.com/""/>
            <outline text=""padgett.com.au"" title=""padgett.com.au""
                type=""rss""
                xmlUrl=""http://padgett.com.au/index.php/feed/"" htmlUrl=""http://padgett.com.au""/>
            <outline text=""Squiggly Brackets"" title=""Squiggly Brackets""
                type=""rss"" xmlUrl=""http://squigglybrackets.me/feed/"" htmlUrl=""http://squigglybrackets.me""/>
        </outline>
        <outline title=""Aussie .NET Bloggers"" text=""Aussie .NET Bloggers"">
            <outline text=""Aaron Cooper"" title=""Aaron Cooper"" type=""rss""
                xmlUrl=""http://www.jroller.com/rss/phloggy"" htmlUrl=""http://www.jroller.com/phloggy/""/>
            <outline text=""Akshay Luther"" title=""Akshay Luther""
                type=""rss""
                xmlUrl=""http://www.superhappycoder.com/blog/index.rdf"" htmlUrl=""http://www.superhappycoder.com/blog/""/>
            <outline text=""Alex Campbell"" title=""Alex Campbell""
                type=""rss""
                xmlUrl=""http://weblogs.asp.net/acampbell/Rss.aspx"" htmlUrl=""http://weblogs.asp.net/acampbell/default.aspx""/>
            <outline text=""Alex Hoffman"" title=""Alex Hoffman"" type=""rss""
                xmlUrl=""http://weblogs.asp.net/ahoffman/Rss.aspx"" htmlUrl=""http://alintex.com/blog/""/>
            <outline text=""Andrew Coates"" title=""Andrew Coates""
                type=""rss"" xmlUrl=""http://blogs.msdn.com/acoat/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/acoat/""/>
            <outline text=""Andrew Delin"" title=""Andrew Delin"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/andrewdelin/rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/andrewdelin/""/>
            <outline text=""Andrew Weaver"" title=""Andrew Weaver""
                type=""rss""
                xmlUrl=""http://blogs.ssw.com.au/andrewweaver/Rss.aspx"" htmlUrl=""http://blogs.ssw.com.au/andrewweaver/""/>
            <outline text=""Anna Liu"" title=""Anna Liu"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/annali/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/annali/""/>
            <outline text=""Anthony Borton"" title=""Anthony Borton""
                type=""rss"" xmlUrl=""http://www.myvstsblog.com/rss.aspx"" htmlUrl=""http://myvstsblog.com/graffiti101/""/>
            <outline text=""Ben's Blog"" title=""Ben's Blog"" type=""rss""
                xmlUrl=""http://www.schmurgon.net/blogs/ben/rss.aspx"" htmlUrl=""http://schmurgon.net/schmurgon/blogs/ben/default.aspx""/>
            <outline text=""Bernard Oh"" title=""Bernard Oh"" type=""rss""
                xmlUrl=""http://www.thespoke.net/MyBlog/Bernard/RssFeed.aspx"" htmlUrl=""http://thespoke.net/blogs/bernard/default.aspx""/>
            <outline text=""Bill Chesnut"" title=""Bill Chesnut"" type=""rss""
                xmlUrl=""http://biztalkbill.com/Rss.aspx"" htmlUrl=""http://biztalkbill.com/Home/tabid/40/BlogId/1/Default.aspx""/>
            <outline text=""Bill McCarthy"" title=""Bill McCarthy""
                type=""rss"" xmlUrl=""http://msmvps.com/bill/Rss.aspx"" htmlUrl=""http://msmvps.com/blogs/bill/default.aspx""/>
            <outline text=""Bruce McLeod"" title=""Bruce McLeod"" type=""rss""
                xmlUrl=""http://www.teknologika.com/blog/index.xml"" htmlUrl=""http://www.teknologika.com""/>
            <outline text=""Cameron Reilly"" title=""Cameron Reilly""
                type=""rss""
                xmlUrl=""http://reilly.typepad.com/cameronreilly/index.rdf"" htmlUrl=""http://reilly.typepad.com/cameronreilly/""/>
            <outline text=""Charles Sterling"" title=""Charles Sterling""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/charles_sterling/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/charles_sterling/""/>
            <outline text=""Chris Burrows"" title=""Chris Burrows""
                type=""rss""
                xmlUrl=""http://madtechnology.net/blog/Rss.aspx"" htmlUrl=""http://madtechnology.spaces.live.com/""/>
            <outline text=""Chris Garty"" title=""Chris Garty"" type=""rss""
                xmlUrl=""http://weblogs.asp.net/chrisgarty/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/chrisgarty/""/>
            <outline text=""Chris Hewitt"" title=""Chris Hewitt"" type=""rss""
                xmlUrl=""http://endintiers.com/blog/Rss.aspx"" htmlUrl=""http://endintiers.com/blog/""/>
            <outline text=""Chris Masters"" title=""Chris Masters""
                type=""rss""
                xmlUrl=""http://chrism.aspxconnection.com/Rss.aspx"" htmlUrl=""http://www.google.com/reader/view/feed%2Fhttp%3A%2F%2Fchrism.aspxconnection.com%2FRss.aspx""/>
            <outline text=""Clarke Scott"" title=""Clarke Scott"" type=""rss""
                xmlUrl=""http://www.clarkescott.com/SyndicationService.asmx/GetRss"" htmlUrl=""http://www.clarkescott.com/""/>
            <outline text=""Daniel Pollard"" title=""Daniel Pollard""
                type=""rss"" xmlUrl=""http://httpcode.com/blogs/Rss.aspx"" htmlUrl=""http://httpcode.com/blogs/""/>
            <outline text=""Daniel Wellesley"" title=""Daniel Wellesley""
                type=""rss""
                xmlUrl=""http://tempurisnacks.blogspot.com/atom.xml"" htmlUrl=""http://tempurisnacks.blogspot.com/""/>
            <outline text=""Darren Gosbell"" title=""Darren Gosbell""
                type=""rss""
                xmlUrl=""http://geekswithblogs.net/darrengosbell/Rss.aspx"" htmlUrl=""http://geekswithblogs.net/darrengosbell/Default.aspx""/>
            <outline text=""Darren Neimke"" title=""Darren Neimke""
                type=""rss"" xmlUrl=""http://markitup.com/rss.aspx"" htmlUrl=""http://markitup.com/""/>
            <outline text=""David Glover"" title=""David Glover"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/dglover/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/dglover/""/>
            <outline text=""David Kean"" title=""David Kean"" type=""rss""
                xmlUrl=""http://davidkean.net/rss.aspx"" htmlUrl=""http://davidkean.net/""/>
            <outline text=""David Lemphers"" title=""David Lemphers""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/davidlem/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/davidlem/""/>
            <outline text=""Deepak Kapoor"" title=""Deepak Kapoor""
                type=""rss""
                xmlUrl=""http://www.deepakkapoor.net/SyndicationService.asmx/GetRss"" htmlUrl=""http://www.deepakkapoor.net/""/>
            <outline text=""Dominic Cooney"" title=""Dominic Cooney""
                type=""rss"" xmlUrl=""http://dcooney.com/Rss.aspx"" htmlUrl=""http://dcooney.com""/>
            <outline text=""Doonesbury"" title=""Doonesbury"" type=""rss""
                xmlUrl=""http://tube2.com/cgi-bin/doonesbury.rb"" htmlUrl=""http://www.doonesbury.com/strip/dailydose/""/>
            <outline text=""Douglas Stockwell"" title=""Douglas Stockwell""
                type=""rss"" xmlUrl=""http://11011.net/index.xml"" htmlUrl=""http://11011.net/""/>
            <outline text=""Dr Neil Roodyn / Blogspot""
                title=""Dr Neil Roodyn / Blogspot"" type=""rss""
                xmlUrl=""http://drneil.blogspot.com/atom.xml"" htmlUrl=""http://drneil.blogspot.com/""/>
            <outline text=""Dr Neil Roodyn / MSN Space""
                title=""Dr Neil Roodyn / MSN Space"" type=""rss""
                xmlUrl=""http://spaces.msn.com/members/hitechhobo/feed.rss"" htmlUrl=""http://hitechhobo.spaces.live.com/""/>
            <outline text=""Dr Peter Stanksi"" title=""Dr Peter Stanksi""
                type=""rss""
                xmlUrl=""http://spaces.msn.com/members/pstanski/feed.rss"" htmlUrl=""http://pstanski.spaces.live.com/""/>
            <outline text=""Eddie De Bear"" title=""Eddie De Bear""
                type=""rss""
                xmlUrl=""http://eddiedebear.blogspot.com/atom.xml"" htmlUrl=""http://eddiedebear.blogspot.com/""/>
            <outline text=""Elaine van Bergen"" title=""Elaine van Bergen""
                type=""rss"" xmlUrl=""http://laneyvb.blogspot.com/atom.xml"" htmlUrl=""http://laneyvb.blogspot.com/""/>
            <outline text=""FrankArr"" title=""FrankArr"" type=""rss""
                xmlUrl=""http://radio.weblogs.com/0124955/rss.xml"" htmlUrl=""http://radio.weblogs.com/0124955/""/>
            <outline text=""Garry"" title=""Garry"" type=""rss""
                xmlUrl=""http://g2007.com/blog/gary/index.xml"" htmlUrl=""http://g2007.com/blog/gary/""/>
            <outline text=""Geoff Appleby"" title=""Geoff Appleby""
                type=""rss""
                xmlUrl=""http://feeds.feedburner.com/geoffappleby"" htmlUrl=""http://codebetter.com""/>
            <outline text=""Geoff Orr Blog"" title=""Geoff Orr Blog""
                type=""rss""
                xmlUrl=""http://spaces.msn.com/geofforr/feed.rss"" htmlUrl=""http://geofforrsyd.wordpress.com""/>
            <outline text=""Graeme Strange"" title=""Graeme Strange""
                type=""rss""
                xmlUrl=""http://spaces.msn.com/members/graemesjagatek/feed.rss"" htmlUrl=""http://graemestrange.wordpress.com""/>
            <outline text=""Grant Holliday"" title=""Grant Holliday""
                type=""rss""
                xmlUrl=""http://www.holliday.com.au/blog/rss.xml"" htmlUrl=""http://ozgrant.com""/>
            <outline text=""Greg Low"" title=""Greg Low"" type=""rss""
                xmlUrl=""http://msmvps.com/GregLow/rss.aspx"" htmlUrl=""http://msmvps.com/blogs/greglow/default.aspx""/>
            <outline text=""James Roe-Smith"" title=""James Roe-Smith""
                type=""rss""
                xmlUrl=""http://www.enigmativity.com/blog/SyndicationService.asmx/GetRss"" htmlUrl=""http://www.enigmativity.com/blog/""/>
            <outline text=""Jarrad Plunkett"" title=""Jarrad Plunkett""
                type=""rss""
                xmlUrl=""http://www.jmpsolutions.com.au/DotNetNuke/Blog/tabid/49/Rss.aspx"" htmlUrl=""http://www.jmpsolutions.com.au/DotNetNuke""/>
            <outline text=""Jason McConnell"" title=""Jason McConnell""
                type=""rss""
                xmlUrl=""http://blogs.msdn.com/jasonmcc/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/jasonmcc/""/>
            <outline text=""Jim Vrckovski"" title=""Jim Vrckovski""
                type=""rss"" xmlUrl=""http://codeinzen.net/Rss.aspx"" htmlUrl=""http://codeinzen.net/""/>
            <outline text=""Joe Sango"" title=""Joe Sango"" type=""rss""
                xmlUrl=""http://msmvps.com/joesango/Rss.aspx"" htmlUrl=""http://msmvps.com/blogs/joesango/default.aspx""/>
            <outline text=""Joel Pobar"" title=""Joel Pobar"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/joelpob/Rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/joelpob/""/>
            <outline text=""John Magner"" title=""John Magner"" type=""rss""
                xmlUrl=""http://withpantscomesdignity.blogspot.com/atom.xml"" htmlUrl=""http://withpantscomesdignity.blogspot.com/""/>
            <outline text=""Joseph Cooney"" title=""Joseph Cooney""
                type=""rss"" xmlUrl=""http://jcooney.net/Rss.aspx"" htmlUrl=""http://jcooney.net/""/>
            <outline text=""Justin King"" title=""Justin King"" type=""rss""
                xmlUrl=""http://geekswithblogs.net/jking/Rss.aspx"" htmlUrl=""http://geekswithblogs.net/jking/Default.aspx""/>
            <outline text=""KarChew Lim"" title=""KarChew Lim"" type=""rss""
                xmlUrl=""http://karchewlim.blogspot.com/atom.xml"" htmlUrl=""http://karchewlim.blogspot.com/""/>
            <outline text=""Karls Blog just powered""
                title=""Karls Blog just powered"" type=""rss""
                xmlUrl=""http://karlkopp.com/blog/SyndicationService.asmx/GetRss"" htmlUrl=""http://karlkopp.com""/>
            <outline text=""Ken Schaefer"" title=""Ken Schaefer"" type=""rss""
                xmlUrl=""http://www.adopenstatic.com/cs/blogs/ken/atom.aspx"" htmlUrl=""http://www.adopenstatic.com/cs/blogs/ken/default.aspx""/>
            <outline text=""LakshmiKanth Upadrasta""
                title=""LakshmiKanth Upadrasta"" type=""rss""
                xmlUrl=""http://kanthu.blogspot.com/atom.xml"" htmlUrl=""http://kanthu.blogspot.com/""/>
            <outline text=""Leon O'Brien"" title=""Leon O'Brien"" type=""rss""
                xmlUrl=""http://cogitativemind.homeip.net/Rss.aspx"" htmlUrl=""http://cogitativemind.homeip.net/""/>
            <outline text=""Luke Drumm"" title=""Luke Drumm"" type=""rss""
                xmlUrl=""http://www.lzcd.com/rss/rss.xml"" htmlUrl=""http://lzcd.com""/>
            <outline text=""Malcolm Groves"" title=""Malcolm Groves""
                type=""rss""
                xmlUrl=""http://blogs.borland.com/malcolmgroves/Rss.aspx"" htmlUrl=""http://blogs.codegear.com/malcolmgroves""/>
            <outline text=""mark cohen"" title=""mark cohen"" type=""rss""
                xmlUrl=""http://mark.silverbullet.com.au/xml-rss2.php"" htmlUrl=""http://mark.silverbullet.com.au/""/>
            <outline text=""Mark Richards"" title=""Mark Richards""
                type=""rss""
                xmlUrl=""http://eaiexpress.com/journey/Rss.aspx"" htmlUrl=""http://eaiexpress.com/journey/""/>
            <outline text=""Mark Richards SoapBox""
                title=""Mark Richards SoapBox"" type=""rss""
                xmlUrl=""http://eaiexpress.com/soapbox/Rss.aspx"" htmlUrl=""http://eaiexpress.com/soapbox/""/>
            <outline text=""Martin Granell"" title=""Martin Granell""
                type=""rss"" xmlUrl=""http://wah.onterra.net/blog/Rss.aspx"" htmlUrl=""http://wah.onterra.net/blog/""/>
            <outline text=""Matt Bourne"" title=""Matt Bourne"" type=""rss""
                xmlUrl=""http://blogs.worldnomads.com.au/matthewb/Rss.aspx"" htmlUrl=""http://blogs.worldnomads.com.au/matthewb/""/>
            <outline text=""Matt Dunn"" title=""Matt Dunn"" type=""rss""
                xmlUrl=""http://mattonsoftware.com/rss.aspx"" htmlUrl=""http://mattonsoftware.com/default.aspx""/>
            <outline text=""Matt Hamilton"" title=""Matt Hamilton""
                type=""rss""
                xmlUrl=""http://www.madprops.org/cs/blogs/mabster/Rss.aspx"" htmlUrl=""http://www.madprops.org/""/>
            <outline text=""Matt Trentini"" title=""Matt Trentini""
                type=""rss""
                xmlUrl=""http://sublimesoftware.blogspot.com/atom.xml"" htmlUrl=""http://sublimesoftware.blogspot.com/""/>
            <outline text=""Matthew Cosier"" title=""Matthew Cosier""
                type=""rss"" xmlUrl=""http://mcosier.blogspot.com/atom.xml"" htmlUrl=""http://mcosier.blogspot.com/""/>
            <outline text=""Michael Kleef:::MSFT""
                title=""Michael Kleef:::MSFT"" type=""rss""
                xmlUrl=""http://blogs.technet.com/mkleef/rss.aspx"" htmlUrl=""http://blogs.technet.com/b/mkleef/""/>
            <outline text=""Mick Badran"" title=""Mick Badran"" type=""rss""
                xmlUrl=""http://www.breezetraining.com.au/site/Portals/0/mickb_Blog_ID_1.xml"" htmlUrl=""http://www.breezetraining.com.au/site/Default.aspx?tabid=30&amp;BlogID=1""/>
            <outline text=""Mike Fitzsimon"" title=""Mike Fitzsimon""
                type=""rss"" xmlUrl=""http://mike.brisgeek.com/feed/"" htmlUrl=""http://mike.brisgeek.com""/>
            <outline text=""Mitch Denny"" title=""Mitch Denny"" type=""rss""
                xmlUrl=""http://notgartner.com/Rss.aspx"" htmlUrl=""http://mitchdenny.com/blog""/>
            <outline text=""Mykre's Space"" title=""Mykre's Space""
                type=""rss""
                xmlUrl=""http://blogs.ircomm.net/mykre/SyndicationService.asmx/GetRss"" htmlUrl=""http://www.google.com/reader/view/feed%2Fhttp%3A%2F%2Fblogs.ircomm.net%2Fmykre%2FSyndicationService.asmx%2FGetRss""/>
            <outline text=""Nick HaC"" title=""Nick HaC"" type=""rss""
                xmlUrl=""http://www.nickhac.com/atom.xml"" htmlUrl=""http://www.nickhac.com/""/>
            <outline text=""Nick Randolph"" title=""Nick Randolph""
                type=""rss""
                xmlUrl=""http://www.thespoke.net/MyBlog/nrandolph/RssFeed.aspx"" htmlUrl=""http://thespoke.net/blogs/nrandolph/default.aspx""/>
            <outline text=""Nick Weinholt"" title=""Nick Weinholt""
                type=""rss""
                xmlUrl=""http://msmvps.com/nickwienholt/rss.aspx"" htmlUrl=""http://msmvps.com/blogs/nickwienholt/default.aspx""/>
            <outline text=""Nigel Watson"" title=""Nigel Watson"" type=""rss""
                xmlUrl=""http://blogs.msdn.com/nigelwat/rss.aspx"" htmlUrl=""http://blogs.msdn.com/b/nigelwat/""/>
            <outline text=""Partial Class"" title=""Partial Class""
                type=""rss""
                xmlUrl=""http://partialclass.wordpress.com/feed/"" htmlUrl=""http://partialclass.wordpress.com""/>
            <outline text=""Paul Glavich"" title=""Paul Glavich"" type=""rss""
                xmlUrl=""http://weblogs.asp.net/pglavich/Rss.aspx"" htmlUrl=""http://weblogs.asp.net/pglavich/default.aspx""/>
            <outline text=""Paul Stovell"" title=""Paul Stovell"" type=""rss""
                xmlUrl=""http://www.paulstovell.net/Rss.aspx"" htmlUrl=""http://www.paulstovell.net/blog""/>
            <outline text=""Paul Webb"" title=""Paul Webb"" type=""rss""
                xmlUrl=""http://thewebfarm.com/blog/Rss.aspx"" htmlUrl=""http://thewebfarm.com/blog/""/>
            <outline text=""Perth .NET Community""
                title=""Perth .NET Community"" type=""rss""
                xmlUrl=""http://spaces.msn.com/members/perthdotnet/feed.rss"" htmlUrl=""http://perthdotnet.spaces.live.com/""/>
            <outline text=""Peter Mackay"" title=""Peter Mackay"" type=""rss""
                xmlUrl=""http://dotnetjunkies.com/WebLog/skyring/Rss.aspx"" htmlUrl=""http://dotnetjunkies.com/WebLog/skyring/default.aspx""/>
            <outline text=""Richard Mason"" title=""Richard Mason""
                type=""rss"" xmlUrl=""http://www.rikware.com/WebLog.rss"" htmlUrl=""http://www.rikware.com/""/>
            <outline text=""Richard's Rant"" title=""Richard's Rant""
                type=""rss""
                xmlUrl=""http://blogs.richardangus.com/blogs/MainFeed.aspx?GroupID=4"" htmlUrl=""http://blogs.richardangus.com/blogs/default.aspx""/>
            <outline text=""Rob Farley"" title=""Rob Farley"" type=""rss""
                xmlUrl=""http://robfarley.blogspot.com/atom.xml"" htmlUrl=""http://robfarley.blogspot.com/""/>
            <outline text=""Rob Johnston"" title=""Rob Johnston"" type=""rss""
                xmlUrl=""http://monach.blogs.com/a_different_view/index.rdf"" htmlUrl=""http://monach.blogs.com/a_different_view/""/>
            <outline text=""Rocky Heckman"" title=""Rocky Heckman""
                type=""rss"" xmlUrl=""http://www.rockyh.net/rss.ashx"" htmlUrl=""http://www.rockyh.net/""/>
            <outline text=""Ross Nelson's Blog""
                title=""Ross Nelson's Blog"" type=""rss""
                xmlUrl=""http://rossnelson.blogspot.com/atom.xml"" htmlUrl=""http://rossnelson.blogspot.com/""/>
            <outline text=""Ryarc Media Systems""
                title=""Ryarc Media Systems"" type=""rss""
                xmlUrl=""http://service.ryarc.com/DasBlog/SyndicationService.asmx/GetRss"" htmlUrl=""http://www.ryarc.com/blog/""/>
            <outline text=""Scott Baldwin"" title=""Scott Baldwin""
                type=""rss""
                xmlUrl=""http://sjbdeveloper.blogspot.com/atom.xml"" htmlUrl=""http://sjbdeveloper.blogspot.com/""/>
            <outline text=""Sean Colyer"" title=""Sean Colyer"" type=""rss""
                xmlUrl=""http://weblogs.asp.net/scolyer/Rss.aspx"" htmlUrl=""http://weblogs.asp.net/scolyer/default.aspx""/>
            <outline text=""Secret Geek"" title=""Secret Geek"" type=""rss""
                xmlUrl=""http://secretgeek.net/rss.asp"" htmlUrl=""http://secretGeek.net/index.asp""/>
            <outline text=""Shaji Sethu"" title=""Shaji Sethu"" type=""rss""
                xmlUrl=""http://shajisethu.blogspot.com/atom.xml"" htmlUrl=""http://shajisethu.blogspot.com/""/>
            <outline text=""ShowUsYourRegex"" title=""ShowUsYourRegex""
                type=""rss""
                xmlUrl=""http://blogs.regexadvice.com/dneimke/Rss.aspx"" htmlUrl=""http://regexadvice.com/blogs/dneimke/default.aspx""/>
            <outline text=""Sydney .NET Usergroup""
                title=""Sydney .NET Usergroup"" type=""rss""
                xmlUrl=""http://blogs.ssw.com.au/usergroup/Rss.aspx"" htmlUrl=""http://blogs.ssw.com.au/usergroup/""/>
            <outline text=""Tatham Oddie"" title=""Tatham Oddie"" type=""rss""
                xmlUrl=""http://www.e-oddie.com/blog/professional/rss.aspx"" htmlUrl=""http://blog.tatham.oddie.com.au""/>
            <outline text=""Team Individualism""
                title=""Team Individualism"" type=""rss""
                xmlUrl=""http://feeds.feedburner.com/TeamIndividualism"" htmlUrl=""http://teamindividualism.blogspot.com/""/>
            <outline text=""Tejas Patel"" title=""Tejas Patel"" type=""rss""
                xmlUrl=""http://geekswithblogs.net/tpatel/Rss.aspx"" htmlUrl=""http://geekswithblogs.net/tpatel/Default.aspx""/>
            <outline text=""The Holistic Web"" title=""The Holistic Web""
                type=""rss""
                xmlUrl=""http://www.useyourweb.com/blog/?feed=rss2"" htmlUrl=""http://www.useyourweb.com/blog""/>
            <outline text=""The Sydney Hacker"" title=""The Sydney Hacker""
                type=""rss""
                xmlUrl=""http://feeds.feedburner.com/TheSydneyHacker"" htmlUrl=""http://www.thesydneyhacker.com/""/>
            <outline text=""Tim Walters"" title=""Tim Walters"" type=""rss""
                xmlUrl=""http://weblogs.asp.net/twalters/Rss.aspx"" htmlUrl=""http://weblogs.asp.net/twalters/default.aspx""/>
            <outline text=""Troy Magennis"" title=""Troy Magennis""
                type=""rss""
                xmlUrl=""http://aspiring.blogs.com/softdev/index.rdf"" htmlUrl=""http://aspiring.blogs.com/softdev/""/>
            <outline text=""WARDY IT Solutions""
                title=""WARDY IT Solutions"" type=""rss""
                xmlUrl=""http://www.wardyit.com/blog/rss.aspx"" htmlUrl=""http://wardyit.com/blog/default.aspx""/>
            <outline text=""William Bartholomew""
                title=""William Bartholomew"" type=""rss""
                xmlUrl=""http://blog.bartholomew.id.au/Rss.aspx"" htmlUrl=""http://blog.bartholomew.id.au/""/>
        </outline>
        <outline title=""design"" text=""design"">
            <outline
                text=""RopeofSilicon.com Movie News, Trailers, Reviews and More""
                title=""RopeofSilicon.com Movie News, Trailers, Reviews and More""
                type=""rss""
                xmlUrl=""http://feeds.feedburner.com/ropeofsilicon/headlines"" htmlUrl=""http://www.ropeofsilicon.com""/>
        </outline>
    </body>
</opml>
");

            foreach (var folder in rootFolder.Folders)
            {
                var folderId = AddFolder(folder);

                foreach (var feed in folder.Feeds)
                {
                    AddFeed(folderId, feed);
                }
            }

            foreach (var feed in rootFolder.Feeds)
            {
                AddFeed(null, feed);
            }

            _context.SaveChanges();

            return "OK";
        }

        private Folder AddFolder(Manager.Folder rssFolder)
        {
            var folder = _context.Folders.FirstOrDefault(f => f.Name == rssFolder.Name);

            if (folder != null)
                return folder;

            folder = _context.Folders.Create();

            folder.Name = rssFolder.Name;
            folder.Id = Guid.NewGuid();

            _context.Folders.Add(folder);

            return folder;
        }

        private void AddFeed(Folder folder, Manager.Feed feed)
        {
            var feedId = Guid.NewGuid();
            var feedUrl = feed.FeedUri.ToString();

            //feed.Load();

            // if feed exists then skip it
            if (_context.Feeds.FirstOrDefault(f => f.FeedUrl == feedUrl) != null)
                return;

            folder.Feeds.Add(new Feed
                {
                    Id = feedId,
                    //FolderId = folderId,
                    Name = feed.Title,
                    FeedUrl = feed.FeedUri.ToString(),
                    HtmlUrl = feed.HtmlUri.ToString()
                    //Items = new Collection<Item>(feed.Items.Select(i => new Item
                    //    {
                    //        Id= Guid.NewGuid(),
                    //        Name = i.Title,
                    //        Raw = i.Content,
                    //        Content = i.Content,
                    //        FeedId = feedId,
                    //        PublishedDateTime = DateTime.Now //i.PublishedDateTime,
                    //    }).ToList())
                });
        }
    }
}
