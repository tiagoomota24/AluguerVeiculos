# Aluguer de Veículos

A web application for managing vehicle rentals, allowing you to efficiently create, view, and manage rental contracts.

## Features

- Registration of customers and vehicles.
- Creating rental contracts.
- Vehicle availability control based on active contracts.
- Automatic update of vehicle status (Available or Rented) based on contract duration.

## Technologies Used

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **HTML/CSS** with **Bootstrap**

---

## Installation

1. **Clone this repository:**
   ```bash
   git clone https://github.com/your-username/your-repository.git
2. **Configure the appsettings.json file with the connection string for SQL Server:
    ```bash
   "ConnectionStrings": {
    "DefaultConnection": "Server=NOME_SERVIDOR;Database=NOME_BASE_DE_DADOS;Trusted_Connection=True;"
    }
3. **Restore NuGet packages:**
   ```bash
   dotnet restore

4. **Run Entity Framework migrations:**
   ```bash
   dotnet ef database update

6. ** Start the project:**
   ```bash
   dotnet run

## Project Structure
- **Models**  
  Define the structure of the project's entities, such as `Customer`, `Vehicle`, and `Contract`.

- **Controllers**  
  Contain the control logic for core resources.

- **Views**  
  Include HTML pages with Razor syntax to render the user interface.

- **Data**  
  Configure the Entity Framework context and handle migrations.

## Contributions

Feel free to contribute to this project by opening issues or submitting pull requests.  
All contributions are welcome and greatly appreciated!

## License

This project is licensed under the **MIT License**.  
See the [LICENSE](./LICENSE) file for more details.
