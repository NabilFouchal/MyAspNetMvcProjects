using MvcMusicStoreApp.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMusicStoreApp.Models
{
    public class MusicStoreDbInitializer : DropCreateDatabaseAlways<MusicStoreDbContext>
    {
        protected override void Seed(MusicStoreDbContext musicStoreDb)
        {
            musicStoreDb.Artists.Add(new Artist { Name = "Al Di Meola" });
            musicStoreDb.Genres.Add(new Genre { Name = "Jazz", Description = "The description of Jazz Genre is ..." });
            musicStoreDb.Albums.Add(new Album
            {
                Artist = new Artist { Name = "Rush" },
                Genre = new Genre { Name = "Rock", Description = "The Description of Rock Music is ..." },
                Price = 1m,
                Title = "Caravan"
            });
            base.Seed(musicStoreDb);
        }
    }
}