# 🌴 RASTALOGIN — Trading System  
*Made by Oskar Gyllenör*

---

### 🪙 About  
Welcome to **Rastalogin**, the digital e-yard where users can trade their treasures, post new items, and manage fair exchanges with others — all through a console-based system written in **C# (.NET 8)**.

Built around solid object-oriented design, this project keeps every piece of logic in its own class.  
Straightforward, organized, and easy to expand — no chaos, no spaghetti.

---

### ⚙️ Features  
Once inside the yard, a user can:

1. Register an account  
2. Log in  
3. Upload information about an item to trade  
4. Browse other users’ items  
5. Request a trade for another user’s item  
6. View incoming trade requests  
7. Accept a trade request  
8. Deny a trade request  
9. Review completed trades  
10. Log out  

Smooth flow, one menu — everything runs from the console.

---

### 🧩 Project Structure  

Ten simple parts working together like a rhythm section:

| File | Purpose |
|------|----------|
| **Users.cs** | Manages user information and login verification |
| **Items.cs** | Holds item name, description, and owner |
| **Trade.cs** | Defines trade objects and status |
| **TradeSystemClasses.cs** | Contains all program logic (user actions, validation, flow) |
| **Program.cs** | Main execution file — the menu and loop |
| **SaveData.cs** | Handles file reading/writing for users, items, and trades |
| **users.txt** | Stores registered users |
| **items.txt** | Stores uploaded items |
| **trades.txt** | Stores trade records |
| **README.md** | Documentation (this file) |

---

### 🧱 Requirements  

Before running, make sure you have:

1. **Visual Studio Code** (1.104.2 or newer)  
2. **.NET SDK 8.0.414**  
3. **Git** installed and configured  

---

### 🖥️ How to Run  

1. **Clone the repository**
   ```bash
   git clone https://github.com/OskarUNLEASHED/RastaTradingPlatform

2. **Enter directory**
   ```bash
   cd RastaTradeSystem

3. **Open the terminal**
   ```bash
   ctrl + shift + ö
   hotkey may vary by different factors, mon.

4. **Run the application**
   ```bash
   dotnet run


### 🎵 Final Words

The Rastalogin Trading System keeps it simple:

No external database.

No confusing frameworks.

If  you have any questions, please reach out to me at oskar.gyllenor@gmail.com

Respect the structure, keep the yard tidy, and trade fair — always.
   


   
