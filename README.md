# PharmaPro APIs

## Overview

SmartPharma is a **smart pharmacy system** designed to automate and enhance pharmacy management. This repository contains the **APIs** that power the system, built using **.NET Core** with a **Clean Architecture** approach.

This project is my **graduation project** from the **Faculty of Information Technology and Computer Science**.

## Features

### 1. **Electronic Prescription System**

- Doctors can create and send **electronic prescriptions** directly to the pharmacy.
- Patients receive prescription details via **email notifications**.
- A **verification code** is sent to patients for added security.

### 2. **Complete Pharmacy Management**

- A **web-based dashboard** for pharmacists to manage inventory, sales, and orders.
- Automated stock management and alerts for low-stock medications.

### 3. **Doctor & Patient Integration**

- Doctors can issue **digital prescriptions** instead of handwritten ones.
- Patients get notified when their medications are ready for pickup.

### 4. **Real-time Notifications**

- Pharmacists receive **alerts** for new prescriptions and stock updates.
- **Email notifications** are sent to patients with their prescriptions.

## Tech Stack

- **.NET Core API** (Back-end)
- **Entity Framework Core** (Database ORM)
- **SQL Server** (Database)
- **C#** (Programming Language)
- **Clean Architecture** (For better code maintainability and scalability)
- **Email Services** (For prescription notifications)

## API Endpoints

### Account

- `POST /api/Account/DoctorLogin`
- `POST /api/Account/DoctorRegister`
- `POST /api/Account/PharmacistLogin`
- `POST /api/Account/PharmacistAdminRegister`
- `POST /api/Account/PharmacistUserRegister`
- `POST /api/Account/ForgetPassword`
- `POST /api/Account/ResetPassword`
- `POST /api/Account/RemoveFromRole`
- `POST /api/Account/AddToRole`

### Doctor

- `GET /api/Doctor/GetAll`
- `GET /api/Doctor/GetById/{id}`
- `POST /api/Doctor/create`
- `PUT /api/Doctor/Update`
- `DELETE /api/Doctor/Delete/{id}`

### Medicine Of Prescription

- `GET /api/MedicineOfPrescription/GetAll`
- `GET /api/MedicineOfPrescription/GetById/{PrescriptionId}/{MedicineId}`
- `GET /api/MedicineOfPrescription/GetByPrescriptionId/{PrescriptionId}`
- `POST /api/MedicineOfPrescription/create`
- `PUT /api/MedicineOfPrescription/Update`
- `DELETE /api/MedicineOfPrescription/Delete/{PrescriptionID}/{MedicineID}`

### Medicines

- `GET /api/Medicines/GetAll`
- `GET /api/Medicines/GetById/{id}`
- `POST /api/Medicines/create`
- `PUT /api/Medicines/Update`
- `DELETE /api/Medicines/Delete/{id}`
- `GET /api/Medicines/GetShelfNumbers`
- `GET /api/Medicines/GetSoonExpiredAndSoonOutOfStock`
- `GET /api/Medicines/GetExpired`
- `GET /api/Medicines/GetOutOfStock`
- `GET /api/Medicines/GetSoonToExpire`
- `GET /api/Medicines/GetSoonOutOfStock`
- `POST /api/Medicines/SendOutStockToEsp`
- `POST /api/Medicines/SendExpiredToEsp`
- `POST /api/Medicines/SendSoonOutOfStockToEsp`
- `POST /api/Medicines/SendSoonToExpireToEsp`

### Order History

- `GET /api/OrderHistory/GetAll`
- `GET /api/OrderHistory/GetById/{id}`
- `POST /api/OrderHistory/create`
- `PUT /api/OrderHistory/Update`
- `DELETE /api/OrderHistory/Delete/{id}`
- `POST /api/OrderHistory/Submit/{Prescriptionid}/{pharmacistid}`

### Patient

- `GET /api/Patient/GetAll`
- `POST /api/Patient/Create`
- `PUT /api/Patient/Update`
- `DELETE /api/Patient/Delete`
- `GET /api/Patient/GetById/{id}`
- `GET /api/Patient/GetByEmail/{Email}`
- `POST /api/Patient/SendVerificationCode/{email}`
- `POST /api/Patient/VerifyCode`
- `GET /api/Patient/GetNames`

### Pharmacist

- `GET /api/Pharmacist/GetAll`
- `GET /api/Pharmacist/GetById/{id}`
- `POST /api/Pharmacist/create`
- `PUT /api/Pharmacist/Update`
- `DELETE /api/Pharmacist/Delete/{id}`

### Prescription

- `GET /api/Prescription/GetAll`
- `GET /api/Prescription/GetById/{id}`
- `GET /api/Prescription/GetByIdWithRelatedData/{id}`
- `GET /api/Prescription/GetByBarcCode`
- `POST /api/Prescription/create`
- `PUT /api/Prescription/Update`
- `DELETE /api/Prescription/Delete/{id}`

### Shelf Status

- `GET /api/ShelfStatus/GetAll`
- `POST /api/ShelfStatus/Create`

## Installation & Setup

1. Clone the repository:

   ```sh
   https://github.com/eeslam99/PharmaPro-APIs.git
   ```

2. Navigate to the project directory:

   ```sh
   cd PharmaPro-APIs
   ```

3. Run database migrations:

   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Start the API:

   ```sh
   dotnet run
   ```

