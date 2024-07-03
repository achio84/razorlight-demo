using RazorLight;
using RazorLightDemo.Model;
using System.Reflection;


var name = "Jong";

_ = $"hello, {name}";
_ = string.Format("hello, {0}", name);

var email = new Email
{
    Name = "jong",
    EmailAddress = "chingchong.jong@etiqa.com.my",
    IdentityNo = "123123123",
    PhoneNo = "0123456789"
};

await FileSourceWithPartialViewFeature(email);


async Task FileSourceFeature(Email email)
{
    var engine = new RazorLightEngineBuilder()
    .UseFileSystemProject("C:\\gitrepo\\RazorLightTemplateDemo\\src\\RazorLightDemo\\FileTemplates")
    .UseMemoryCachingProvider()
    .Build();

    var result = await engine.CompileRenderAsync("FileTemplate.cshtml", email);

    Console.WriteLine(result);
}
async Task FileSourceWithPartialViewFeature(Email email)
{
    var engine = new RazorLightEngineBuilder()
    .UseFileSystemProject("C:\\gitrepo\\RazorLightTemplateDemo\\src\\RazorLightDemo\\FileTemplates")
    .UseMemoryCachingProvider()
    .Build();

    var result = await engine.CompileRenderAsync("FileTemplateWithPartialView.cshtml", email);

    Console.WriteLine(result);
}

async Task EmbeddedResourceFeature(Email email)
{
    var engine = new RazorLightEngineBuilder()
    .UseEmbeddedResourcesProject(typeof(Program).Assembly, "RazorLightDemo.EmbeddedTemplates")
    .UseMemoryCachingProvider()
    .Build();

    var result = await engine.CompileRenderAsync("EmbeddedTemplate", email);

    Console.WriteLine(result);
}

async Task EmbeddedResourceWithPartialViewFeature(Email email)
{
    var engine = new RazorLightEngineBuilder()
    .UseEmbeddedResourcesProject(typeof(Program).Assembly, "RazorLightDemo.EmbeddedTemplates")
    .UseMemoryCachingProvider()
    .Build();

    var result = await engine.CompileRenderAsync("EmbeddedTemplateWithPartialView", email);

    Console.WriteLine(result);
}

async Task CachedTemplateFeature()
{
    var email = new Email
    {
        Name = "jong",
        EmailAddress = "chingchong.jong@etiqa.com.my",
        IdentityNo = "123123123",
        PhoneNo = "0123456789"
    };
    
    var email2 = new Email
    {
        Name = "johnny",
        EmailAddress = "johnny@etiqa.com.my",
        IdentityNo = "123123123",
        PhoneNo = "123123123"
    };

    var engine = new RazorLightEngineBuilder()
    .UseEmbeddedResourcesProject(typeof(Program).Assembly, "RazorLightDemo.EmbeddedTemplates")
    .UseMemoryCachingProvider()
    .Build();

    var result = await engine.CompileRenderAsync("EmbeddedTemplate", email);

    var cached = engine.Handler.Cache.RetrieveTemplate("EmbeddedTemplate");

    if (cached.Success)
    {
        var result2 = await engine.RenderTemplateAsync(cached.Template.TemplatePageFactory(), email);

        Console.WriteLine(result2);
    }
    else
    {
        var result1 = await engine.CompileRenderAsync("EmbeddedTemplate", email);

        Console.WriteLine(result1);
    }

}