# Zotes API

A minimal .NET 9 Web API for managing notes protected by per-user API keys.

- Base route prefix: `/api/v1`
- Protected endpoints require the `X-Api-Key` header
- In-memory databases are used for both app data and Identity (no external DB required)

## Prerequisites

- .NET SDK 9.0+
- A dev HTTPS certificate trusted locally
    - One-time setup on your machine: `dotnet dev-certs https --trust`

## Project details

- This project uses in-memory databases and clears data on each run.
- Password policy: at least 8 chars, 1 digit, 1 lowercase, 1 uppercase (no special char required).
- Security header name is defined as `X-Api-Key`.

## Start the project

From the repo root:

```shell
# Restore and run the API
dotnet restore src
dotnet run --project src/Zotes.Api --launch-profile https
```

## Test the API

### Using the bundled HTTP client file

If you use JetBrains Rider, Visual Studio or VS Code REST Client, you can run the prepared requests:

- Open [assets/requests.http](assets/requests.http) in your editor
- Execute requests top to bottom:
    - Register
    - Login
    - Issue API Key
    - Notes CRUD
- The script stores `apiKey` and created `noteId` automatically for subsequent calls

### Using cURL from the command line

#### Register a user and get an API key

##### 1) Register

```shell
curl -X POST https://localhost:7001/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "john.doe@example.com",
    "password": "P@ssw0rdA",
    "firstName": "John",
    "lastName": "Doe"
  }'
```

##### 2) Login - validates credentials (Optional)

```shell
curl -X POST https://localhost:7001/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "john.doe@example.com",
    "password": "P@ssw0rdA"
  }'
```

##### 3) Issue an API key – use your email/password

```shell
# Create a new API key
curl -s -X POST https://localhost:7001/api/v1/auth/api-keys \
  -H "Content-Type: application/json" \
  -d '{
    "email": "john.doe@example.com",
    "password": "P@ssw0rdA"
  }'
```

The response will contain your API key:

```json
{
  "apiKey": "<YOUR_API_KEY>",
  "expiresAtUtc": "<EXPIRATION_DATE>"
}
```

#### Calling protected CRUD endpoints for Notes

All protected requests must include the header `X-Api-Key: <YOUR_API_KEY>`.

##### Set your API key in an environment variable

```shell
export API_KEY="<YOUR_API_KEY>"
```

##### Create a note

```shell
curl -s -X POST https://localhost:7001/api/v1/notes \
  -H "Content-Type: application/json" \
  -H "X-Api-Key: $API_KEY" \
  -d '{
    "title": "My first note",
    "content": "Hello world"
  }'
```

##### List notes

```shell
curl -s https://localhost:7001/api/v1/notes \
  -H "X-Api-Key: $API_KEY"
```

##### Set the note id in an environment variable

```shell
export NOTE_ID="<YOUR_NOTE_ID>"
```

##### Get a note by id

```shell
curl -s https://localhost:7001/api/v1/notes/$NOTE_ID \
  -H "X-Api-Key: $API_KEY"
```

##### Update a note

```shell
curl -s -X PUT https://localhost:7001/api/v1/notes/$NOTE_ID \
  -H "Content-Type: application/json" \
  -H "X-Api-Key: $API_KEY" \
  -d '{
    "title": "Updated title",
    "content": "Updated content"
  }'
```

##### Delete a note

```shell
curl -s -X DELETE https://localhost:7001/api/v1/notes/$NOTE_ID \
  -H "X-Api-Key: $API_KEY"
```
