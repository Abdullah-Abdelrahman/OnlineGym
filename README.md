# Fitness Coaching Management System

## Project Overview

The **Fitness Coaching Management System** is designed to streamline the workflow of fitness coaching teams and enhance the user experience for clients. The system replaces traditional methods like WhatsApp and PDF with a more interactive and efficient platform.

### Key Features:

1. **User Interface**:
   - A simple, clear, and interactive interface that allows users to track their subscriptions, training plans, daily workouts, and meal plans.

2. **Admin Dashboard**:
   - A comprehensive dashboard that allows admins to:
     - Manage staff performance and salaries.
     - Control subscriptions by adding, updating, or removing them.
     - Block or ban any user.
     - Track and manage orders, including CRUD operations on various system entities such as workouts and videos.
     - Access statistics such as most popular subscriptions and top-spending customers.

3. **Employee Functionality**:
   - After receiving an order from the admin, employees can:
     - Schedule appointments with clients.
     - Create personalized training plans.
     - Modify the start date before the plan begins.

4. **User Management**:
   - Users can update their email and password at any time.

---

## Technologies Used:

### Back-End:
- **ASP.NET Core MVC**: Built using the 3-tier architecture for scalability and separation of concerns.

### Front-End:
- **HTML, CSS, JavaScript**: For a responsive and interactive user interface.
- **jQuery**: Utilized for dynamic updates to partial views without the need to reload the entire page.

---

## Implemented Topics:

- **Repository Pattern** (with Generic Repositories)
- **Unit of Work**
- **Asynchronous Programming**
- **Identity and External Login**
- **Stripe Payment Integration**
- **Response Caching**
- **Hangfire** for background jobs and task scheduling

---

## How to Set Up the Project:

1. **Clone the Repository**:
2.**Ensure you have the .NET SDK installed**:
3.**Stripe Integration**:
   Set up your Stripe keys in the appsettings.json or use environment variables for a secure configuration:
   "Stripe": {
  "SecretKey": "your_secret_key",
  "PublishableKey": "your_publishable_key"
}

