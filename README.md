
# Yet Another ToDo Application (YATA)

Project for learning web application development with .NET and Angular

## Requirements

- .NET CLI v8
- .NET SDK v8
- Node.js v18+

You can also use [Github Codespaces](https://codespaces.new/jsteinshouer/dotnet-todo).

## Getting Started

### Backend

```
cd ToDoApi
dotnet run
```

### Front-end

```
cd ToDoApp
npm install
npm run start
```

## Testing

### xUnit

You can run them in the CLI or use the Test Explorer in VS Code.

```
cd ToDoApi.Test/
dotnet test
```

### Playwright e2e tests

```
cd ToDoApp.Test
npm install
```

You can run the tests via the test explorer or CLI.

```
npx playwright tests
```

With UI

```
npx playwright tests --ui
```

**TODO**
- [ ] Implement a more robust authentication solution for e2e tests
