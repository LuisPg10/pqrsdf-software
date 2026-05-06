# Proyecto PQRSDF - Backend

Este proyecto es una solución robusta y escalable para la gestión de Peticiones, Quejas, Reclamos, Sugerencias, Denuncias y Felicitaciones (PQRSDF). Está construido siguiendo los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**.

## 1. Descripción breve
El sistema permite a los clientes registrar solicitudes de diversos tipos y áreas, y proporciona a los funcionarios administrativos las herramientas para asignar, gestionar y responder a dichas solicitudes, manteniendo un historial completo de interacciones.

## 2. Cómo correrlo

### Requisitos previos
- **Docker** y **Docker Compose**.
- **.NET 10 SDK**.

### Pasos para la ejecución
1. **Levantar la infraestructura**:
   Ejecuta el siguiente comando en la raíz del proyecto para iniciar la base de datos SQL Server:
   ```bash
   docker-compose up -d
   ```

2. **Configurar la base de datos**:
   Asegúrate de que la cadena de conexión en `API/appsettings.Development.json` sea correcta. Por defecto es:
   `"Server=localhost;Database=PQRSDF;User ID=SA;Password=MyStrongPass123;TrustServerCertificate=True"`

3. **Ejecutar la aplicación**:
   Desde la carpeta `backend`, ejecuta:
   ```bash
   dotnet run --project API
   ```
   *Nota: Las migraciones se aplican automáticamente al iniciar la aplicación en el entorno de Desarrollo.*

4. **Acceder a la documentación**:
   Una vez en ejecución, puedes explorar los endpoints a través de **Scalar** en:
   `https://localhost:7026/docs`.

## 3. Decisiones técnicas tomadas

- **Stack Tecnológico**:
  - **.NET 10**: Para aprovechar mejoras de rendimiento y características modernas de C#.
  - **MediatR**: Implementación del patrón Mediador para desacoplar la lógica de aplicación (Handlers) de los controladores de API, facilitando el mantenimiento y el cumplimiento de CQRS.
  - **Entity Framework Core**: ORM para la persistencia de datos con un enfoque Code-First.
  - **Mapster**: Elegido por su alto rendimiento y facilidad de configuración en comparación con otros mapeadores de objetos.
  - **ErrorOr**: Para el manejo de errores de forma funcional, evitando el uso excesivo de excepciones para el flujo de control.
  - **JWT (JSON Web Tokens)**: Para asegurar la comunicación y manejar la autorización basada en roles (Admin, Functionary).
  - **Scalar**: Elegido en lugar de Swagger por ser una alternativa más moderna, visualmente atractiva y muy amigable con el desarrollo y consumo de APIs.


- **Forma de Trazabilidad**:
  Se implementó mediante **Eventos de Dominio**. Cada vez que ocurre un cambio significativo en una entidad (por ejemplo, al crear una solicitud o cambiar su estado), se dispara un evento. Estos eventos se capturan en el `SaveChangesAsync` del `ApplicationDbContext` y se publican a través de MediatR antes de confirmar la transacción. Esto asegura que la auditoría y los efectos secundarios sean coherentes y desacoplados.

## 4. Qué NO se alcanzó a hacer
- **Generación de número de radicado**: No se implementó la lógica para generar automáticamente el número de radicado único por solicitud; actualmente se maneja de forma simplificada o manual.
- **Cálculo de días de expiración**: No se alcanzó a realizar el cálculo automático de días para determinar la fecha exacta en la que una PQRSDF expira según su tipo y normativa.
- **Seed de datos**: No se incluyó la carga inicial automática de datos (usuarios admin predeterminados, tipos de solicitudes o áreas configuradas), lo que requiere una configuración manual inicial en la base de datos.
- **Pruebas Automatizadas**: Debido a restricciones de tiempo, no se incluyó la suite de pruebas unitarias o de integración para validar los casos de borde.

## 5. Lo que se haría con más tiempo (Roadmap)
1. **Generador de Radicados**: Implementar un servicio robusto para generar números de radicado con un formato profesional (ej. `2026-12398745`).
2. **Cálculo Automático de Vencimientos**: Desarrollar la lógica para determinar la fecha límite de respuesta basada en los términos legales de cada tipo de PQRSDF.
3. **Seeding Robusto**: Implementar una clase `DbInitializer` para que el proyecto sea "ready-to-go" tras la primera ejecución.
4. **Frontend en React**: Implementar una interfaz de usuario con React para una mejor experiencia de usuario.
