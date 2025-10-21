Architecture Diagram for Production
===================================

In production, you'll include a reverse-proxy to eliminate CORS configuration and run all the containers from the same sub-domain.

```mermaid
graph TB
    %% Services
    gateway[API Gateway<br/>YARP :8080]
    frontend[Frontend app hosting<br/>React/Vue/Blazor/etc]
    frameworkApi[Framework API<br/>ASP.NET WebAPI]
    funcVoteGet[Vote Get Function<br/>Azure Function]
    funcVoteScore[Vote Score Function<br/>Azure Function]
    redis[(Redis Cache)]
    pgweb[PgWeb UI]
    redisCommander[Redis Commander]

    %% PostgreSQL grouped tables
    subgraph postgresDB[PostgreSQL]
        direction TB
        frameworksTable[(Frameworks table)]
        votesTable[(Votes table)]
    end

    %% Database connections
    frameworksTable --> pgweb
    votesTable --> pgweb
    redis --> redisCommander

    %% Gateway routes
    gateway --> |/| frontend
    gateway --> |/api/vote/get| funcVoteGet
    gateway --> |/api/**| frameworkApi
    gateway --> |POST /api/vote/score/**| funcVoteScore

    frameworkApi --> frameworksTable
    frameworkApi --> redis

    funcVoteGet --> votesTable
    funcVoteGet --> redis

    funcVoteScore --> votesTable

    user(User's Browser runs frontend) --> gateway

    %% Styling
    classDef database fill:#f9f,stroke:#333,stroke-width:2px
    classDef frontend fill:#bbf,stroke:#333,stroke-width:2px
    classDef gateway fill:#bfb,stroke:#333,stroke-width:2px
    classDef function fill:#ffb,stroke:#333,stroke-width:2px
    classDef api fill:#fbf,stroke:#333,stroke-width:2px
    classDef ui fill:#ddd,stroke:#333,stroke-width:2px

    class postgres,redis database
    class frontend frontend
    class gateway gateway
    class funcVoteGet,funcVoteScore function
    class frameworkApi api
    class pgweb,redisCommander ui
```
