# Visual Cryptography with Enveloping by Digital Watermarking

A robust system that encrypts image-based secret information using visual cryptography, combined with invisible digital watermarking for enhanced security.

---

## 📚 **Table of Contents**
1. [Abstract](#abstract)
2. [Features](#features)
3. [Screenshots](#screenshots)
4. [How It Works](#how-it-works)
5. [Technologies Used](#technologies-used)
6. [Requirements](#requirements)
7. [Applications](#applications)
8. [Future Scope](#future-scope)
9. [Installation](#installation)
10. [Contributors](#contributors)

---

## 📄 **Abstract**

Visual Cryptography is an encryption technique designed to obscure image-based secret information. In this system:
- Images are divided into multiple **shares** using secret-sharing schemes.
- Each share is **enveloped** in other images through invisible digital watermarking (LSB replacement).
- Decryption is performed using **k-out-of-n shares** and an OR operation to reconstruct the image visually.

This ensures high security and usability without requiring cryptographic computations during decryption.

---

## ✨ **Features**
- **Enhanced Security**: Secret shares are enveloped using digital watermarking.
- **Ease of Use**: Decryption can be performed visually, without computational effort.
- **Efficiency**: Faster encryption and decryption processes.
- **Cross-Platform Sharing**: Shares can be transmitted via email or FAX.

---

## 📸 **Screenshots**

- **Login Form**:  
  ![Login Form](https://github.com/Muskan-Varma/Visual/blob/main/screenshots/login.png)

- **Registration Form**:  
  ![Registration Form](https://github.com/Muskan-Varma/Visual/blob/main/screenshots/registration.png)

- **Main Form**:  
  ![Main Form](https://github.com/Muskan-Varma/Visual/blob/main/screenshots/main_form.png)

- **Encryption Form**:  
  ![Encryption Form](https://github.com/Muskan-Varma/Visual/blob/main/screenshots/encryption.png)

- **Decryption Form**:  
  ![Decryption Form](https://github.com/Muskan-Varma/Visual/blob/main/screenshots/decryption.png)

---

## ⚙️ **How It Works**

1. **Encryption**:
   - Input an image to encrypt.
   - Specify the number of **shares (n)** and the minimum shares required to decrypt **(k)**.
   - The secret image is divided into shares and embedded in cover images using LSB replacement.

2. **Decryption**:
   - Collect at least **k** shares.
   - Perform an OR operation on the shares to reconstruct the original image.

3. **Watermarking**:
   - Invisible watermarking ensures shares appear as normal images to mislead attackers.

---

## 🛠️ **Technologies Used**
- **Language**: C#  
- **Framework**: .NET Framework  
- **Database**: Firebase Realtime Database  
- **IDE**: Visual Studio (2010 or above)

---

## 💻 **Requirements**

### Software Requirements:
- **Operating System**: Windows 7/8 or higher  
- **IDE**: Visual Studio (2010 or above)  
- **Database**: Firebase  

### Hardware Requirements:
- **Processor**: 1.8 GHz or faster  
- **Memory**: Minimum 2 GB RAM  
- **Storage**: Minimum 10 GB available  
- **Display**: 1280 x 720 resolution  

---

## 🌟 **Applications**
- **Biometric Security**  
- **Digital Watermarking**  
- **Steganography**  
- **Remote Electronic Voting**

---

## 🚀 **Future Scope**
- Explore robust signature extraction methods to recover modified images without originals.  
- Optimize pixel expansion for better aspect ratio preservation.  
- Enhance security by integrating advanced random number generators.  

---

## 🛠️ **Installation**

1. Clone the repository:
   ```bash
   git clone https://github.com/Muskan-Varma/Visual.git
   ```
2. Open the project in Visual Studio.
3. Configure Firebase Realtime Database for backend operations.
4. Build and run the solution.

---

## 👥 **Contributors**
- **Muskan Varma**  
- **Smita Chinchole**  
- **Nikita Shinde**  
- **Mayuri Avhad**
