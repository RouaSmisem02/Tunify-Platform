# Tunify Platform

Tunify Platform is a web application designed to manage and stream music content. The platform allows users to create playlists, manage subscriptions, and explore various artists, albums, and songs. This project is built using ASP.NET Core with Entity Framework Core for database management.

## Features

- **User Registration and Authentication**: Secure user registration and login with ASP.NET Identity.
- **Subscription Management**: Users can subscribe to different plans (e.g., Free, Premium).
- **Playlist Creation and Management**: Users can create and manage playlists.
- **Music Streaming**: Stream songs from various artists and albums.
- **Explore Artists, Albums, and Songs**: Browse and discover content by artists, albums, or song genres.

## Entity-Relationship Diagram (ERD)

![Tunify Platform ERD](https://github.com/user-attachments/assets/9985457b-9b6a-46c9-9f1e-8c3de898d0e9)

## Database Models and Relationships

### Users
- **UserId**: Primary key
- **UserName**: The user's name
- **Email**: The user's email address
- **JoinDate**: The date the user joined the platform
- **SubscriptionId**: Foreign key linking to the Subscription table

### Subscriptions
- **SubscriptionId**: Primary key
- **SubscriptionType**: Type of subscription (e.g., Free, Premium)
- **Price**: Subscription cost

### Playlists
- **PlaylistId**: Primary key
- **PlaylistName**: Name of the playlist
- **CreatedDate**: Date the playlist was created
- **UserId**: Foreign key linking to the Users table

### PlaylistSongs
- **PlaylistSongId**: Primary key
- **SongId**: Foreign key linking to the Songs table
- **PlaylistId**: Foreign key linking to the Playlists table

### Songs
- **SongId**: Primary key
- **Title**: Title of the song
- **ArtistId**: Foreign key linking to the Artists table
- **AlbumId**: Foreign key linking to the Albums table
- **Duration**: Length of the song in seconds
- **Genre**: Genre of the song

### Artists
- **ArtistId**: Primary key
- **Name**: Name of the artist
- **Bio**: Biography of the artist

### Albums
- **AlbumId**: Primary key
- **Title**: Title of the album
- **ReleasedOn**: Release date of the album
- **ArtistId**: Foreign key linking to the Artists table

## Relationships Overview

- **Users** can have multiple **Playlists**.
- **Playlists** can contain multiple **Songs** through the **PlaylistSongs** junction table.
- **Songs** belong to an **Artist** and an **Album**.
- **Albums** are created by **Artists**.
- **Users** subscribe to the platform through a **Subscription**.

## Setup and Configuration

1. **Clone the Repository**: Clone the repository from GitHub.
   ```bash
   git clone https://github.com/your-repo/Tunify-Platform.git
