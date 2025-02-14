# Financial Transactions Processing App

This application processes financial transactions, stores them in a SQL Server database, and generates an analytical report in JSON format. The solution is containerized using Docker.

## 🛠️ Features

- **Data Loading**: Loads a large dataset of transactions from a CSV file.
- **Data Storage**: Stores transactions in SQL Server using optimized batch inserts.
- **Data Analysis**:
  - Calculates total income and expenses for each user.
  - Identifies the top-3 categories by the number of transactions.
  - Finds the user with the highest total expenses.
- **JSON Report Generation**: Generates a JSON report with the analysis results.

---

## 🚀 Prerequisites

Before running the application, ensure you have the following installed:

- Docker
- Docker Compose

---

## 🔧 Setup & Configuration

### 1. Clone the Repository

```sh
git clone https://github.com/k0les1aw/dataprocessingtest.git
git checkout main
cd dataprocessing
```

### 2. Build and Run the Application with Docker Compose

Run the following command to start the services (SQL Server and the .NET App) using Docker Compose:

```sh
docker-compose up --build -d
```

### 3. Configuration

The application uses SQL Server as the backend database. The database connection string is configured in the `.NET` application via the `appsettings.json` file:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver_container; MultiSubnetFailover=True; Database=TransactionsDB; User Id=sa; Password=12345678xX!; TrustServerCertificate=True; Encrypt=false"
  }
```

The application uses `data/` folder in receive files to process. Specific filename is configured via the `docker-compose.yml` file in `command` section:

```yaml
  command: ["transactions_10_thousand.csv"]
```

### 4. Folders

- In the `data/` folder you should place *.csv files to process.
- The `reports/` folder is mounted as a volume to `/app/reports` inside the Docker container. Generated **JSON report** (`TransactionReport.json`) will be saved to the `reports/` folder on your host machine.

---

## 💻 How to Use

1. **Place the CSV File**: Ensure your CSV file is ready and accessible, place it in `data/` folder. Confilgure filename in `docker-compose.yml` file.
2. **Run the App**: The app will automatically start processing and load transactions from the CSV file into SQL Server.
3. **Wait for the Report**: The JSON report is generated and saved in the `reports/` folder.

---

## 📝 Example JSON Report Structure

After the app runs, the generated JSON report (`TransactionReport.json`) will have the following structure:

```json
{
  "UsersSummary": [
    {
      "UserId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "TotalIncome": 5000.00,
      "TotalExpense": -1200.50
    }
  ],
  "TopCategories": [
    { "Category": "Groceries", "TransactionsCount": 150 },
    { "Category": "Transport", "TransactionsCount": 120 },
    { "Category": "Salary", "TransactionsCount": 100 }
  ],
  "HighestSpender": {
    "UserId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "TotalIncome": 5000.00,
    "TotalExpense": -1200.50
  }
}
```

---

## 👥 Contributing

Feel free to fork the repository, submit issues, or open pull requests.

---

## 📚 License

This project is licensed under the MIT License.

