## Phase 1: Foundation Setup

### Issue #1: Create Shared Modal Component
```yaml
title: "[Foundation] Create Shared Modal Component for CRUD Operations"
body: |
  ## Description
  Create a reusable modal component that will be used by all CRUD operations across the application.

  ## Tasks
  - Create `/Views/Shared/_Modal.cshtml` with Bootstrap modal markup
  - Add modal ID `form-modal` with header, body, and footer sections
  - Include the modal in `/Views/Shared/_Layout.cshtml`
  - Ensure modal is compatible with existing `showInPopup()` function in site.js

  ## Acceptance Criteria
  - Modal renders correctly on all pages
  - Modal works with existing AJAX functions
  - Modal is responsive on mobile devices
```

### Issue #2: Create Generic Repository Implementation
```yaml
title: "[Foundation] Implement Generic Repository Pattern"
body: |
  ## Description
  Create the generic repository implementation following the ITasklyne<T> interface.

  ## Tasks
  - Create `/Infrastructure/Data/Tasklyne.cs`
  - Implement `ITasklyne<T>` interface with methods:
    - `GetAllAsync()`
    - `GetByIdAsync(int id)`
    - `GetFirstOrDefaultAsync(filter, includeProperties)`
    - `Add(T entity)`
    - `Remove(T entity)`
    - `RemoveRange(IEnumerable<T> entities)`
  - Use ApplicationDbContext for data operations

  ## Acceptance Criteria
  - All interface methods implemented
  - Code follows C# 12 primary constructor pattern
  - Async/await patterns used correctly
```

### Issue #3: Create UnitOfWork Implementation
```yaml
title: "[Foundation] Implement Unit of Work Pattern"
body: |
  ## Description
  Create UnitOfWork implementation to manage multiple repositories and transactions.

  ## Tasks
  - Create `/Infrastructure/Data/UnitOfWork.cs`
  - Implement `IUnitOfWork` interface
  - Expose repositories for:
    - Departments
    - Employees
    - Projects
    - ProjectTasks
    - AssignedTasks
  - Implement `SaveAsync()` method

  ## Acceptance Criteria
  - All repositories exposed as properties
  - SaveAsync commits changes to database
  - Follows dependency injection patterns
```

### Issue #4: Integrate AdminLTE Layout
```yaml
title: "[Foundation] Integrate AdminLTE Template with Layout"
body: |
  ## Description
  Replace the default Bootstrap layout with AdminLTE wrapper layout.

  ## Tasks
  - Update `/Views/Shared/_Layout.cshtml` with AdminLTE structure
  - Include AdminLTE CSS from `/adminlte/dist/css/adminlte.min.css`
  - Include AdminLTE JS from `/adminlte/dist/js/adminlte.min.js`
  - Add AdminLTE wrapper, navbar, sidebar, footer
  - Ensure responsive behavior on mobile/tablet/desktop

  ## Acceptance Criteria
  - AdminLTE layout displays correctly
  - Responsive design works on all devices
  - All existing content renders properly in new layout
```

---

## Phase 2: Department Module

### Issue #5: Complete Department Controller
```yaml
title: "[Department] Complete DepartmentController CRUD Operations"
body: |
  ## Description
  Implement full CRUD operations for Department management using Bootstrap Modal + AJAX pattern.

  ## Tasks
  - Update `/Controllers/DepartmentController.cs`
  - Implement `Index` action - list all departments
  - Implement `AddOrEdit` GET - return partial view for modal
  - Implement `AddOrEdit` POST - create/update with JSON response
  - Implement `Delete` POST - delete department with confirmation

  ## Reference
  - Follow the pattern in `RolesController.cs:7-84`

  ## Acceptance Criteria
  - All actions implemented following RolesController pattern
  - JSON responses return `{ isValid: true }` on success
  - Validation errors return partial view with error messages
```

### Issue #6: Create Department Views
```yaml
title: "[Department] Create Department Views with Modal Integration"
body: |
  ## Description
  Create Index and AddOrEdit views for Department module.

  ## Tasks
  - Create `/Views/Department/Index.cshtml`
    - AdminLTE card with card-header and card-tools
    - DataTable or table with departments list
    - "New Department" button using `showInPopup()`
    - Edit/Delete actions per row
  - Create `/Views/Department/AddOrEdit.cshtml` (partial view)
    - Form with Name, Description fields
    - Modal footer with Close/Save buttons
    - Validation scripts included

  ## Acceptance Criteria
  - Index view displays departments in table
  - New/Edit buttons open modal
  - Form submits via AJAX without page reload
  - Validation displays inline in modal
```

---

## Phase 3: Employee Module

### Issue #7: Create Employee Controller
```yaml
title: "[Employee] Create EmployeeController with CRUD"
body: |
  ## Description
  Create EmployeeController with full CRUD operations using UserManager and UnitOfWork.

  ## Tasks
  - Create `/Controllers/EmployeeController.cs`
  - Inject `UserManager<Employee>`, `UnitOfWork`
  - Implement actions:
    - `Index` - list employees with department filter
    - `AddOrEdit` GET/POST - create/update employee
    - `Delete` POST - delete employee
    - `Details` - view employee details

  ## Acceptance Criteria
  - UserManager used for user creation/management
  - Department filter working on Index
  - Password hashed correctly on creation
  - AJAX JSON responses for modal operations
```

### Issue #8: Create Employee Views
```yaml
title: "[Employee] Create Employee Views"
body: |
  ## Description
  Create views for Employee management module.

  ## Tasks
  - Create `/Views/Employee/Index.cshtml`
    - Employee table with AdminLTE avatars
    - Department badge column
    - Filter by department dropdown
  - Create `/Views/Employee/AddOrEdit.cshtml` (partial view)
    - Fields: FullName, Nickname, Department (dropdown), Email, Password (new only)

  ## Acceptance Criteria
  - AdminLTE avatar images display correctly
  - Department filter updates table via AJAX
  - Password field only shows for new employees
  - Department dropdown populated from database
```

---

## Phase 4: Project Module

### Issue #9: Create Project Controller
```yaml
title: "[Project] Create ProjectController with CRUD"
body: |
  ## Description
  Create ProjectController with CRUD operations and department assignment.

  ## Tasks
  - Create `/Controllers/ProjectController.cs`
  - Inject `UnitOfWork`
  - Implement actions:
    - `Index` - list projects
    - `AddOrEdit` GET/POST - create/update projects
    - `Delete` POST - delete project

  ## Acceptance Criteria
  - Multi-select department assignment handled
  - Project-department relationships saved correctly
  - AJAX modal operations working
```

### Issue #10: Create Project Views
```yaml
title: "[Project] Create Project Views"
body: |
  ## Description
  Create views for Project management module.

  ## Tasks
  - Create `/Views/Project/Index.cshtml`
    - Project table with department badges
    - Task count per project displayed
    - Start date formatted correctly
  - Create `/Views/Project/AddOrEdit.cshtml` (partial view)
    - Fields: Name, Description, StartDate
    - Multi-select dropdown for departments

  ## Acceptance Criteria
  - Departments show as badges in list
  - Multi-select dropdown works correctly
  - Date picker for StartDate field
  - Task count updates dynamically
```

---

## Phase 5: ProjectTask Module

### Issue #11: Create ProjectTask Controller
```yaml
title: "[ProjectTask] Create ProjectTaskController with CRUD"
body: |
  ## Description
  Create ProjectTaskController with CRUD and project filtering.

  ## Tasks
  - Create `/Controllers/ProjectTaskController.cs`
  - Inject `UnitOfWork`, `UserManager<Employee>`
  - Implement actions:
    - `Index` - list all tasks
    - `ByProject` - list tasks filtered by project
    - `AddOrEdit` GET/POST - create/update tasks
    - `Delete` POST - delete task

  ## Acceptance Criteria
  - Project filter working
  - Creator set to current user automatically
  - AJAX modal operations working
```

### Issue #12: Create ProjectTask Views
```yaml
title: "[ProjectTask] Create ProjectTask Views"
body: |
  ## Description
  Create views for ProjectTask management module.

  ## Tasks
  - Create `/Views/ProjectTask/Index.cshtml`
    - Tasks grouped/filtered by project
    - Show creator and due date columns
    - Badge for overdue tasks (red)
  - Create `/Views/ProjectTask/AddOrEdit.cshtml` (partial view)
    - Fields: Name, Description, DueDate, Project (dropdown)
    - Creator auto-set to current user (hidden field)

  ## Acceptance Criteria
  - Project filter updates table
  - Overdue tasks highlighted in red
  - DueDate field uses date picker
  - Creator auto-populated
```

---

## Phase 6: AssignedTask Module

### Issue #13: Create AssignedTask Controller
```yaml
title: "[AssignedTask] Create AssignedTaskController with Workflow"
body: |
  ## Description
  Create AssignedTaskController with task assignment and review workflow.

  ## Tasks
  - Create `/Controllers/AssignedTaskController.cs`
  - Inject `UnitOfWork`, `UserManager<Employee>`
  - Implement actions:
    - `Index` - all assigned tasks list
    - `AddOrEdit` GET/POST - assign task to employee
    - `UpdateStatus` GET/POST - update task status
    - `SubmitForReview` POST - submit completed task
    - `Review` GET/POST - manager review (approve/reject)
    - `MyTasks` - tasks for current user
    - `ForReview` - tasks pending manager review

  ## Acceptance Criteria
  - Status workflow: NotStarted → InProgress → Completed
  - Review workflow: Completed → Pending → Approved/Rejected
  - Role-based access to actions
```

### Issue #14: Create AssignedTask Views
```yaml
title: "[AssignedTask] Create AssignedTask Views"
body: |
  ## Description
  Create views for AssignedTask module with workflow UI.

  ## Tasks
  - Create `/Views/AssignedTask/Index.cshtml`
    - Task list with status badges
    - Filter by status dropdown
    - Filter by employee dropdown
  - Create `/Views/AssignedTask/AddOrEdit.cshtml` (partial view)
    - Fields: TaskList, Employee, DueDate, Priority
  - Create `/Views/AssignedTask/UpdateStatus.cshtml` (partial view)
    - Status dropdown
    - Comments field
  - Create `/Views/AssignedTask/Review.cshtml` (partial view)
    - Review form with Approve/Reject buttons
    - Comments field for rejection

  ## Acceptance Criteria
  - Status badges color-coded
  - Priority badges (High=red, Medium=yellow, Low=green)
  - Review workflow works end-to-end
  - Comments saved on status/review changes
```

---

## Phase 7: Dashboard & Navigation

### Issue #15: Update Dashboard Controller
```yaml
title: "[Dashboard] Update HomeController with Statistics"
body: |
  ## Description
  Update HomeController to show dashboard statistics.

  ## Tasks
  - Update `/Controllers/HomeController.cs`
  - Add statistics to Index action:
    - Total projects count
    - Total tasks count
    - Total employees count
    - Task completion rate per department
    - Pending reviews count
    - Overdue tasks list
  - Pass statistics to View via ViewData/ViewModel

  ## Acceptance Criteria
  - All statistics calculated correctly
  - Data comes from database via UnitOfWork
  - Statistics update in real-time
```

### Issue #16: Create Dashboard View
```yaml
title: "[Dashboard] Create AdminLTE Dashboard View"
body: |
  ## Description
  Create professional dashboard using AdminLTE widgets.

  ## Tasks
  - Update `/Views/Home/Index.cshtml`
  - Use AdminLTE components:
    - Small boxes for key statistics (4 boxes)
    - Info boxes for detailed metrics
    - Task list widget for pending items
    - Profile widget for current user
  - Add charts if time permits (Chart.js)

  ## Acceptance Criteria
  - Dashboard displays all statistics
  - AdminLTE widgets render correctly
  - Responsive layout
  - Data displays in visually appealing format
```

### Issue #17: Update Navigation Sidebar
```yaml
title: "[Navigation] Update Sidebar with All Module Links"
body: |
  ## Description
  Update sidebar with all module links and role-based visibility.

  ## Tasks
  - Update `/Views/Shared/_sidebar.cshtml`
  - Add menu items:
    - Dashboard (Home)
    - Departments
    - Employees
    - Projects
    - Tasks (ProjectTask)
    - My Tasks (AssignedTask - Employee/Manager)
    - Reviews (AssignedTask - Manager only)
    - Roles (Admin only)
  - Add role-based visibility using `User.IsInRole()`
  - Add FontAwesome icons for each item

  ## Acceptance Criteria
  - All menu items link to correct actions
  - Role-based visibility working:
    - Admin sees all
    - Manager sees Dashboard, Projects, Tasks, Reviews
    - Employee sees Dashboard, My Tasks
  - Icons display correctly
```

---

## Phase 8: Polish & UX

### Issue #18: Enhance JavaScript for AJAX
```yaml
title: "[UX] Enhance site.js with Better AJAX Handling"
body: |
  ## Description
  Improve site.js with better loading states and error handling.

  ## Tasks
  - Update `/wwwroot/js/site.js`
  - Add `jQueryAjaxPost()` function
  - Add loading spinner during AJAX calls
  - Add toast notifications for success/error
  - Improve error handling with user-friendly messages

  ## Acceptance Criteria
  - Loading spinners show during operations
  - Success/error toasts display
  - Form buttons disabled during submission
  - Error messages are user-friendly
```

### Issue #19: Integrate DataTables
```yaml
title: "[UX] Add DataTables to All List Views"
body: |
  ## Description
  Integrate DataTables plugin for better table functionality.

  ## Tasks
  - Add DataTables CSS/JS from AdminLTE
  - Initialize DataTable on:
    - Department Index
    - Employee Index
    - Project Index
    - ProjectTask Index
    - AssignedTask Index
  - Enable: search, pagination, sorting
  - Add server-side processing if needed

  ## Acceptance Criteria
  - DataTables initialized on all index views
  - Search, pagination, sorting working
  - Responsive tables on mobile
  - AJAX operations still work with DataTables
```

### Issue #20: Add SweetAlert2 for Confirmations
```yaml
title: "[UX] Replace Native Confirm with SweetAlert2"
body: |
  ## Description
  Integrate SweetAlert2 for better looking confirmations and notifications.

  ## Tasks
  - Add SweetAlert2 CSS/JS to layout
  - Replace all `confirm()` calls with Swal.fire()
  - Update delete buttons to use SweetAlert
  - Add success toast on successful operations
  - Add error toast on failures

  ## Acceptance Criteria
  - All delete confirmations use SweetAlert
  - Success toasts show on CRUD operations
  - Error toasts show on failures
  - Consistent styling across all modals
```

---

## Bonus / Enhancement Issues

### Issue #21: Add Profile Page
```yaml
title: "[Enhancement] Add User Profile Page"
body: |
  ## Description
  Create a profile page for users to view and edit their information.

  ## Tasks
  - Create ProfileController
  - Create profile view with user info
  - Allow editing FullName, Nickname
  - Show assigned tasks summary
  - Show task completion stats

  ## Acceptance Criteria
  - Profile page displays user information
  - User can update their profile
  - Task statistics accurate
```

### Issue #22: Add Task Comments Feature
```yaml
title: "[Enhancement] Add Comments to AssignedTask"
body: |
  ## Description
  Add ability to add comments to tasks for collaboration.

  ## Tasks
  - Create TaskComment entity
  - Add migration for new table
  - Create comments section on task detail/view
  - Add AJAX comment form
  - Show comment history

  ## Acceptance Criteria
  - Users can add comments to tasks
  - Comment history displays
  - AJAX submission without reload
```

---

## Summary: Quick Reference

| Issue | Title | Phase |
|-------|-------|-------|
| #1 | Shared Modal Component | Foundation |
| #2 | Generic Repository | Foundation |
| #3 | UnitOfWork | Foundation |
| #4 | AdminLTE Layout | Foundation |
| #5 | Department Controller | Department |
| #6 | Department Views | Department |
| #7 | Employee Controller | Employee |
| #8 | Employee Views | Employee |
| #9 | Project Controller | Project |
| #10 | Project Views | Project |
| #11 | ProjectTask Controller | ProjectTask |
| #12 | ProjectTask Views | ProjectTask |
| #13 | AssignedTask Controller | AssignedTask |
| #14 | AssignedTask Views | AssignedTask |
| #15 | Dashboard Controller | Dashboard |
| #16 | Dashboard View | Dashboard |
| #17 | Navigation Sidebar | Dashboard |
| #18 | Enhanced JavaScript | Polish |
| #19 | DataTables Integration | Polish |
| #20 | SweetAlert2 Integration | Polish |

**Total: 20 core issues + 2 enhancement issues**