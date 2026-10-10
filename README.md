# Patient Portal
Web application that allows users to search and view patient information.

## Instructions to run
1. Clone the repository.

2. Create a Google OAuth 2.0 client and add this authorized redirect URI:
   `http://localhost:5013/signin-google`

3. Set the OAuth credentials using .NET User Secrets:
  ```Powershell
    dotnet user-secrets set "Authentication:ClientId" "<your-client-id>" --project PatientPortal.Web
  ```
  ```Powershell
    dotnet user-secrets set "Authentication:ClientSecret" "<your-client-secret>" --project PatientPortal.Web
  ```

4. Run the app with the included `http` launch profile:
  ```Powershell
    dotnet run --project PatientPortal.Web --launch-profile http
  ```

5. Open `http://localhost:5013` and use the sign-in link to access the protected pages.

## Data

The app creates its SQLite database and seeds sample patient records when it starts.

## Technologies Used
### Backend
- ASP.NET Core 10
- C#
- Razor Pages

### Frontend
- HTML
- Tailwind CSS
- JavaScript

### Authentication
- Google Authentication

### Database
- SQLite

### Development Tools
- Visual Studio 2026

## Notes
The Razor Page models interact directly with the service layer on this project to keep things straightforward and avoid having a lot of boilerplate. Additionally, the application is configured to run strictly over HTTP to simplify local testing and eliminate any potential SSL/TLS local certificate issues. In a production application, I'd use DTOs, separate validation and enforce HTTPS. I skipped them here to keep the code lean, fast to build, and easy to read.

### AI Use
I used a bit of AI to speed up the tedious parts of building this. Specifically, I used it to help map out the Tailwind CSS styles on the frontend and to generate the mock C# seed data for the database. 
