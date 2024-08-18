using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

public class PlaylistRepositoryTests
{
    private readonly PlaylistRepository _repository;
    private readonly Mock<DbSet<Playlists>> _mockPlaylistsSet;
    private readonly Mock<TunifyDbContext> _mockContext;

    public PlaylistRepositoryTests()
    {
        _mockContext = new Mock<TunifyDbContext>();
        _mockPlaylistsSet = new Mock<DbSet<Playlists>>();

        _mockContext.Setup(c => c.Playlists).Returns(_mockPlaylistsSet.Object);
        _repository = new PlaylistRepository(_mockContext.Object);
    }

    [Fact]
    public async Task GetAllPlaylistsAsync_ReturnsAllPlaylists()
    {
        // Arrange
        var playlists = new List<Playlists>
        {
            new Playlists { Id = 1, Name = "Playlist 1" },
            new Playlists { Id = 2, Name = "Playlist 2" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Playlists>>();
        mockSet.As<IQueryable<Playlists>>().Setup(m => m.Provider).Returns(playlists.Provider);
        mockSet.As<IQueryable<Playlists>>().Setup(m => m.Expression).Returns(playlists.Expression);
        mockSet.As<IQueryable<Playlists>>().Setup(m => m.ElementType).Returns(playlists.ElementType);
        mockSet.As<IQueryable<Playlists>>().Setup(m => m.GetEnumerator()).Returns(playlists.GetEnumerator());

        _mockContext.Setup(c => c.Playlists).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetAllPlaylistsAsync();

        // Assert
        var playlistsList = result.ToList();
        Assert.Equal(2, playlistsList.Count);
        Assert.Contains(playlistsList, p => p.Name == "Playlist 1");
        Assert.Contains(playlistsList, p => p.Name == "Playlist 2");
    }
}
