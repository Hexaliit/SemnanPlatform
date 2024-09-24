# Semnan University Platform API
In this project Masters can upload a course.courses can be free and not free.every course has some vidoe and videos can be shon on demo or not.Admins can also upload courses and videos plus cayegories.Users can watch free course's videos or buy not free courses and watch videos.

# Key Features
- **Authentication**
- **Buy Course**
- **Watch Vidoes**
## On Admin Side
- **Charge Balance**
- **Change Role**
- **CRUD operation on Categories**
- **CRUD operation on Courses**
- **CRUD operation on Videos**

# Used Technologies
- Asp.Net Core Web Api
- .Net 7
- Entity Framework Core
- Automapper
- Serilog
- MediatR
- Fluent Validation
- Swagger UI
- JWT(TSON Web Tokens)

# Design Patterns
- **RESTful Principles:** Adhering to RESTful design principles to ensure that APIs are designed for simplicity, scalability, and ease of use.
- **Repository Pattern:** Implementing the repository pattern to abstract the data layer, enhancing application maintainability, testability, and cleanliness by decoupling data access logic from business logic.
- **Dependency Injection:** is a programming technique that makes a class independent of its dependencies.
- **CQRS:** separates read and update operations for a data store. Implementing CQRS  can maximize performance, scalability, and security.

# Architecture
- **Clean Architecture**
  - **External Layers:**
    - Presentation: Controllers for handling requests and managing client-server communication.
    - Infrastructure: Manages external resources such as databases and auth management.
  - **Core Layers:**
    - Application Layer: Implements business logic and orchestrates interactions between components.
    - Domain Layer: Contains fundamental business rules and entities, independent of external concerns like databases or user interfaces.

# ScreenShots
![Screenshot of a comment on a GitHub issue showing an image, added in the Markdown, of an Octocat smiling and raising a tentacle.](/src/SemnanCourse.API/wwwroot/ScreenShots/1.PNG)
![Screenshot of a comment on a GitHub issue showing an image, added in the Markdown, of an Octocat smiling and raising a tentacle.](/src/SemnanCourse.API/wwwroot/ScreenShots/2.PNG)
![Screenshot of a comment on a GitHub issue showing an image, added in the Markdown, of an Octocat smiling and raising a tentacle.](/src/SemnanCourse.API/wwwroot/ScreenShots/3.PNG)
