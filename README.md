# 🎮 Catch That Coin – GameFi on Somnia

Catch That Coin is a **blockchain-powered GameFi experience** built using Unity and the Thirdweb SDK, fully integrated with the **Somnia Testnet**. Dive into a fun, token-gated environment where you collect coins, spin to multiply rewards, and earn real value with STT tokens.

---

## 🚀 Features

- 🔓 **Token Gate NFT Access** – Only holders of the Token Gate NFT can access gameplay.  
- 👑 **VIP NFT System** – Unlock elite perks, fast-track progression, and access VIP-only areas.  
- 💎 **GEM Token (ERC20)** – Use GEM to spin for bonus rewards.  
- 🎰 **Spin-to-Earn Mechanic** – Spend GEM for a random multiplier, boosting your score.  
- 💰 **Earn STT Token** – Final score converts to STT at a 1000:1 ratio (e.g., 1000 Score = 1 STT).  
- 🧩 **Web3 Onboarding** – Integrated MetaMask connection and blockchain transactions.  
- 🌐 **Runs on Somnia Testnet** – Fast, cost-efficient game chain integration.  
- 🧠 **AI Data Analytics (via Somnia Data Stream)** – Game data recorded and analyzed in real-time using GPT-4.1-mini.

---

## 🛠️ Tech Stack

- 🎮 Unity Engine  
- 🔷 [Thirdweb Unity SDK](https://portal.thirdweb.com/unity)  
- 🌍 Somnia Testnet  
- ⚙️ [Somnia Data Stream SDK](https://docs.somnia.network)  
- 🧠 OpenAI GPT-4.1-mini (via backend)  
- 🔐 Smart contracts deployed via Thirdweb  

---

## 🔗 Smart Contracts on Somnia

### 🎫 Token Gate NFT  
- **Name**: The Token Gate NFT  
- **Symbol**: `GATE`  
- **Description**: Grants access to core gameplay areas and exclusive features.  
- **Address**: [`0xE91D3C51390BA023b99BfEF7faC98774c5Bb21cA`](https://thirdweb.com/team/kelvincod/0e7eed2e2e708515a11d78eaedf37f02/contract/50312/0xE91D3C51390BA023b99BfEF7faC98774c5Bb21cA)  
- **Claim Condition**: Free  

---

### 🧧 VIP NFT  
- **Name**: The VIP NFT  
- **Symbol**: `VIP`  
- **Description**: Grants VIP privileges – early access, exclusive areas, faster progression.  
- **Address**: [`0x72b4567f3AB0a7cdD6Cd64bc35882F0Cbff53925`](https://thirdweb.com/team/kelvincod/0e7eed2e2e708515a11d78eaedf37f02/contract/50312/0x72b4567f3AB0a7cdD6Cd64bc35882F0Cbff53925)  
- **Claim Condition**: 0.1 STT  

---

### 💎 GEM Token (ERC20)  
- **Name**: The GEM Token  
- **Symbol**: `GEM`  
- **Description**: Used for spinning rewards, boosting scores, and participating in events.  
- **Address**: [`0xF47943625e94be853DD9EA7976125353E93dC5f3`](https://thirdweb.com/team/kelvincod/0e7eed2e2e708515a11d78eaedf37f02/contract/50312/0xF47943625e94be853DD9EA7976125353E93dC5f3)  
- **Claim Condition**: 0.01 STT  

---

## 🧠 Somnia Data Stream + AI Integration

### 🔹 Overview

In *Catch That Coin*, every player’s wallet and score are sent to the **Somnia Data Stream** after each run.  
A separate **backend service** (see: [Somnia-Datastream-with-AI-Backend](https://github.com/linHDev0106/Somnia-Datastream-with-AI-Backend)) then retrieves and analyzes this data using **OpenAI GPT-4.1-mini**.

The AI acts as a **virtual performance coach**, providing feedback, insights, and encouragement based on the player’s on-chain history.

---

### 🔹 Architecture Flow

```
[ Unity Game ]
     ↓
[ POST /api/publish ]
     ↓
[ Somnia Data Stream ]
     ↓
[ AI Backend (GPT-4.1-mini) ]
     ↓
[ Player Feedback + Analytics Dashboard ]
```

---

### 🔹 How It Works in Game

1. **Game Ends → Data Recorded**  
   When a match ends, the Unity client sends a `POST` request to the backend:  
   ```json
   { "player": "0xA24d7E...", "score": 450 }
   ```

2. **On-Chain Data Stream**  
   The backend encodes this into Somnia’s schema:  
   ```
   address player
   uint256 score
   ```

3. **AI Feedback**  
   When the player opens the *Performance* screen, the backend fetches their data and runs an AI analysis via:  
   ```
   GET /api/data?wallet=<player_wallet>
   ```
   **Example Response:**  
   ```json
   {
     "totalEntries": 5,
     "data": [
       { "player": "0xA24d7E...", "score": 250 },
       { "player": "0xA24d7E...", "score": 450 }
     ],
     "aiSummary": "🏆 Overview: Your performance is improving fast! Keep pushing for the top 10%!"
   }
   ```

4. **Display in Unity**  
   The response is shown inside a UI panel as an **AI Summary Card**, motivating players to improve their next run.

---

### 🔹 Backend Repo Reference

🔗 [Somnia-Datastream-with-AI-Backend](https://github.com/linHDev0106/Somnia-Datastream-with-AI-Backend)  
> Node.js + Express backend connected to Somnia Data Stream and GPT-4.1-mini.

**Main Endpoints:**  
| Endpoint | Method | Description |
|-----------|--------|-------------|
| `/api/publish` | POST | Record wallet + score data |
| `/api/data` | GET | Fetch and analyze all scores for a player |
| `/api/schema` | GET | Retrieve active data schema |

---

### 🔹 Benefits

- 📊 **Transparent and verifiable data** – Every player’s performance is stored on-chain.  
- 🤖 **Smart feedback** – Personalized AI commentary for each player.  
- 🔥 **Community leaderboard potential** – Compare progress across wallet addresses.  
- 🧠 **Player retention** – Motivational summaries drive engagement and replayability.

---

## 📦 How to Run Locally

1. **Clone the repository**  
   ```bash
   git clone https://github.com/linHDev0106/Catch-That-Coin-GameFi-On-Somnia.git
   cd Catch-That-Coin-GameFi-On-Somnia
   ```

2. **Open in Unity**  
   Open the project using Unity Editor (2022.x or newer recommended).  
   > 📦 `Thirdweb Unity SDK` is already included.

3. **Update Contract & API Endpoints**  
   - Update NFT and ERC20 contract addresses in your Unity ScriptableObjects.  
   - In your Web3 Manager or GameManager script, set backend URL (e.g. `http://localhost:3000/api/publish`).  

4. **Play & Test**  
   - Connect wallet (MetaMask) when prompted.  
   - Ensure you’re on the **Somnia Testnet**.  
   - Play, collect, spin, and view AI analysis after each match!

---

## 🙌 Credits

- Built by: **linHDev0106 Team**  
- Powered by: [Thirdweb](https://thirdweb.com/) + [Somnia Network](https://docs.somnia.network)  
- AI Analytics: [Somnia Datastream with AI Backend](https://github.com/linHDev0106/Somnia-Datastream-with-AI-Backend)

---

## 📨 Contact

Want to contribute or collaborate? Feel free to open an issue or reach out!  

---

## 🪄 License

Released under the **MIT License**.
