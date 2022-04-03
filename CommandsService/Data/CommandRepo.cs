using CommandsService.Models;

namespace CommandsService.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() >= 0;
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public async Task CreatePlatform(Platform platform)
    {
        platform = platform ?? throw new ArgumentNullException(nameof(platform));

        await _context.Platforms.AddAsync(platform);
    }

    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(p => p.Id == platformId);
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _context.Commands
                       .Where(c => c.PlatformId == platformId)
                       .OrderBy(c => c.Platform.Name);
    }

    public Command? GetCommand(int platformId, int commandId)
    {
        return _context.Commands
                       .FirstOrDefault(c => c.PlatformId == platformId && c.Id == commandId);
    }

    public async Task CreateCommand(int platformId, Command command)
    {
        command = command ?? throw new ArgumentNullException(nameof(command));

        command.PlatformId = platformId;
        await _context.Commands.AddAsync(command);
    }
}