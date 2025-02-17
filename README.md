# API Testing with Playwright & C#

## Features
- API testing using **Playwright with C#**
- **Authentication** handling using JWT tokens
- **API chaining** to test multiple operations in a single test
- **XUnit framework** for structuring test cases
- **Assertions** for validating API responses

## Project Structure
```
📦 API-Testing-Playwright-CSharp
 ┣ 📂 Tests
 ┃ ┣ 📜 BookingApiTests.cs    # Test cases for API operations
 ┃ ┣ 📜 AuthTests.cs          # Test cases for authentication
 ┣ 📂 Models
 ┃ ┣ 📜 BookingModel.cs       # Model for request/response mapping
 ┃ ┣ 📜 AuthModel.cs          # Model for authentication data
 ┣ 📜 README.md               # Project documentation
```

## Setup Instructions
### Prerequisites
Ensure you have the following installed:
- .NET 6.0 or later
- Playwright for .NET (`dotnet add package Microsoft.Playwright`)
- XUnit (`dotnet add package xunit`)

### Installation
1. Clone this repository:
   ```sh
   git clone https://github.com/YadvirJaswal/Automated-Restful-Booker-API-Tests-using-Playwright
   ```
2. Navigate to the project folder:
   ```sh
   cd RestFul Booker Api Tests
   ```
3. Install dependencies:
   ```sh
   dotnet restore
   ```
4. Build the project:
   ```sh
   dotnet build
   ```

## Running Tests
To execute all test cases, run:
```sh
dotnet test
```

To run a specific test:
```sh
dotnet test --filter "FullyQualifiedName=Namespace.ClassName.MethodName"
```

## API Operations
This project tests the following **Restful Booker API** operations:
1. **Create Booking** (`POST /booking`)
2. **Get Booking** (`GET /booking/{id}`)
3. **Update Booking** (`PUT /booking/{id}` with JWT token)
4. **Delete Booking** (`DELETE /booking/{id}` with JWT token)

## API Chaining Implementation
API chaining is implemented by creating **separate methods** for each operation and calling them in a single test case:
```csharp
[Fact]
public async Task BookingApiChaining_ShouldWorkCorrectly()
{
    int bookingId = await CreateBooking();
    var bookingDetails = await GetBooking(bookingId);
    bool isUpdated = await UpdateBooking(bookingId);
    bool isDeleted = await DeleteBooking(bookingId);
    Assert.True(isDeleted);
}
```

## Contributing
Feel free to submit **issues** or **pull requests** if you find any improvements or bugs!

---

⭐ **If you like this project, give it a star on GitHub!**

