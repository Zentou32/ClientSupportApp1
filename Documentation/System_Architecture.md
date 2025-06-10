# Архитектура

## 1. Слои
- **Презентационный**: Окна WPF (XAML)  
- **Домен**: Классы моделей  
- **Данные**: ADO.NET репозитории  

## 2. Схема данных
```plantuml
@startuml
entity Users {
  +Id [PK]
  --
  Login
  Password
  Role
}

entity Achievements {
  +Id [PK]
  --
  UserId [FK]
  CompetitionName
  Points
}
@enduml