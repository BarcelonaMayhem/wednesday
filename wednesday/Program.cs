using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Picture> repo = [];

app.MapGet("/", () => repo);
app.MapPost("/add", (Picture dto) => repo.Add(dto));
app.MapPut("/update", ([FromQuery] int id, UpdatePictureDTO dto) =>
    {
        Picture buffer = repo.Find(p => p.Id == id);
        if (buffer != null)
        {
            if(buffer.Id != dto.id)
                buffer.Id = dto.id;
            if (buffer.Name != dto.name)
                buffer.Name = dto.name;
            if (buffer.Type != dto.type)
                buffer.Type = dto.type;
            if (buffer.Size != dto.size)
                buffer.Size = dto.size;
            return Results.Json(buffer);
        }
        else
        {
            return Results.NotFound();
        }
});
app.MapDelete("/delete/", ([FromQuery] int id) =>
    {
        Picture buffer = repo.Find(p => p.Id == id);
        repo.Remove(buffer);
    });
app.Run();

public class Picture
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public DateTime DateCreated { get; set; }
};

record class UpdatePictureDTO (int id, string name, string type, string size);