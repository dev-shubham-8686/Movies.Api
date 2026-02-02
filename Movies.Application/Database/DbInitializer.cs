using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            await connection.ExecuteAsync("""
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieRating]') AND type in (N'U'))
        CREATE TABLE [dbo].[MovieRating](
        	[MovieId] [uniqueidentifier] NOT NULL,
        	[UserId] [uniqueidentifier] NOT NULL,
        	[Rating] [int] NULL,
         CONSTRAINT [PK_MovieRating] PRIMARY KEY CLUSTERED 
        (
        	[MovieId] ASC,
        	[UserId] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ) ON [PRIMARY]
        """);

            await connection.ExecuteAsync("""
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieActor]') AND type in (N'U'))
        CREATE TABLE [dbo].[MovieActor](
        	[MovieId] [nvarchar](60) NOT NULL,
        	[ActorId] [nvarchar](60) NOT NULL,
         CONSTRAINT [PK_MovieActor] PRIMARY KEY CLUSTERED 
        (
        	[MovieId] ASC,
        	[ActorId] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ) ON [PRIMARY]
        """);

            await connection.ExecuteAsync("""
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movie]') AND type in (N'U'))
        CREATE TABLE [dbo].[Movie](
        	[Id] [uniqueidentifier] NOT NULL,
        	[Title] [nvarchar](max) NULL,
        	[YearOfRelease] [int] NULL,
         CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
        (
        	[Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        """);

            await connection.ExecuteAsync("""
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actor]') AND type in (N'U'))
        CREATE TABLE [dbo].[Actor](
        	[Id] [uniqueidentifier] NOT NULL,
        	[Name] [nvarchar](max) NULL,
        	[LastName] [nvarchar](max) NULL,
        	[Age] [int] NULL,
         CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED 
        (
        	[Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        """);
        }
    }
}
