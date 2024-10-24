# Brazilian Electronic Voting Machine Clone

This project is a clone of the Brazilian electronic voting machine, originally developed for my "Programming Language 1" course in college. The application simulates the voting process used in official Brazilian elections. After completing the academic project, I enhanced it further as a personal project to add additional features and improve the user experience.

The voting machine is currently set to simulate a **second-round mayoral election**, where the candidates are:
- **Candidate 1**: Number **10**
- **Candidate 2**: Number **20**

If the voter enters a number other than **10** or **20**, the vote will be counted as **null**. There is also an option to cast a **blank vote**.

---

## Features
- Voting system with two candidates for a second-round election.
- Simulates an authentic Brazilian electronic voting experience.
- Option to vote **null** by entering an invalid number.
- Option to vote **blank**.
- Simple and intuitive graphical user interface.
- Real-time vote counting and display of results.
- Option to reset or close the voting process.

---

## Technologies Used
- **C#**
- **Visual Studio .Net 8.0**
- **Windows Forms**
- Built and tested on **Windows OS**.

---

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/JoaoVittorIshida/brazilian-voting-machine-clone.git
    ```

2. Open the project in **Visual Studio**.
3. Restore NuGet packages if required.
4. Build and run the project.

---

## How to Use

1. Start the application to load the voting screen.
2. Enter the candidate's number (**10** or **20**) and press "Confirm" to cast a vote.
3. If a number other than **10** or **20** is entered, the vote will be counted as **null**.
4. Press "White Vote" to cast a blank vote.
5. After confirming a vote, the application will ask if you want to continue voting.
    - If **Yes**, the screen will reset, allowing for the next vote.
    - If **No**, the voting will end, and a screen with the final results will be displayed.
6. After viewing the results, the application will automatically close.

---

## Screenshot
![Screenshot of the Voting Machine](https://github.com/user-attachments/assets/8130884f-95ab-490e-bf3e-fbf35b3f173c)



---

## Authors

- João Vittor Ishida de Sousa - [GitHub](https://github.com/JoaoVittorIshida)
