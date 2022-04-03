using CommandsService.Models;

namespace CommandsService.Data;

public interface ICommandRepo
{
    Task<bool> SaveChanges();

    // Platforms
    IEnumerable<Platform> GetAllPlatforms();
    Task CreatePlatform(Platform platform);
    bool PlatformExists(int platformId);
    
    // Commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);
    Command? GetCommand(int platformId, int commandId);
    Task CreateCommand(int platformId, Command command);
}