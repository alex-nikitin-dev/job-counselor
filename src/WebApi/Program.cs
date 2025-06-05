var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "JobTrack.ResumeBuilder API");

app.Run();
