# Tunify Platform

Tunify Platform is a web application designed to manage and stream music content. The platform allows users to create playlists, manage subscriptions, and explore various artists, albums, and songs. This project is built using ASP.NET Core with Entity Framework Core for database management.

## Entity-Relationship Diagram (ERD)

![Tunify Platform ERD](https://github.com/user-attachments/assets/9985457b-9b6a-46c9-9f1e-8c3de898d0e9)

## Database Models and Relationships

### Users
- **UsersId**: Primary key
- **UserName**: The user's name
- **Email**: The user's email address
- **JoinDate**: The date the user joined the platform
- **SubscriptionId**: Foreign key linking to the Subscription table

### Subscriptions
- **SubscriptionId**: Primary key
- **SubscriptionType**: Type of subscription (e.g., Free, Premium)
- **Price**: Subscription cost

### Playlists
- **PlaylistsId**: Primary key
- **PlaylistsName**: Name of the playlist
- **CreatedDate**: Date the playlist was created
- **UsersId**: Foreign key linking to the Users table

### PlaylistSongs
- **PlaylistSongsId**: Primary key
- **SongsId**: Foreign key linking to the Songs table
- **PlaylistsId**: Foreign key linking to the Playlists table

### Songs
- **SongsId**: Primary key
- **Title**: Title of the song
- **ArtistsId**: Foreign key linking to the Artists table
- **AlbumsId**: Foreign key linking to the Albums table
- **Duration**: Length of the song in seconds
- **Genre**: Genre of the song

### Artists
- **ArtistsId**: Primary key
- **Name**: Name of the artist
- **Bio**: Biography of the artist

### Albums
- **AlbumsId**: Primary key
- **Title**: Title of the album
- **ReleasedOn**: Release date of the album
- **ArtistsId**: Foreign key linking to the Artists table

## Relationships Overview

- **Users** can have multiple **Playlists**.
- **Playlists** can contain multiple **Songs** through the **PlaylistSongs** junction table.
- **Songs** belong to an **Artist** and an **Album**.
- **Albums** are created by **Artists**.
- **Users** subscribe to the platform through a **Subscription**.

## Setup and Configuration

1. **Clone the Repository**: Clone the repository from GitHub.

2. **Install NuGet Packages**: Install the necessary NuGet packages using the following commands:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
### Usage
Swagger API Documentation: The API is documented using Swagger. Once the application is running, you can access the Swagger UI at http://localhost:5000/swagger to explore and test the API endpoints.
Additional Information
For more detailed information about the project structure, features, and contributions, please refer to the full documentation in the repository.
