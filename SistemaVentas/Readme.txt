# Sistema de Ventas - API REST

API REST desarrollada en .NET 10 para gestión de ventas, clientes y productos.

## Tecnologías Utilizadas

- **.NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core 10**
- **Oracle Database**
- **JWT Authentication** (Bearer Token)
- **Swagger/OpenAPI** - Documentación interactiva
- **FluentValidation** - Validación de modelos
- **Serilog** - Logging

## Requisitos Previos

- .NET 10 SDK
- Oracle Database
- Postman (opcional, para pruebas)

## Configuración

### 1. Clonar el repositorio
```bash
git clone [URL_DEL_REPOSITORIO]
cd SistemaVentas
```

### 2. Configurar la cadena de conexión

Edita el archivo `appsettings.json` y configura tu conexión a Oracle:
```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=TU_USUARIO;Password=TU_PASSWORD;Data Source=TU_DATASOURCE"
  }
}
```

### 3. Aplicar migraciones
```bash
dotnet ef database update
```

### 4. Ejecutar la aplicación
```bash
dotnet run
```

La API estará disponible en: `https://localhost:7XXX` o `http://localhost:5031`

## Documentación de la API

Una vez ejecutada la aplicación, accede a Swagger UI:
```
http://localhost:5031/swagger
```

## Autenticación

La API utiliza JWT (JSON Web Tokens) para autenticación.

### Obtener Token

**Endpoint:** `POST /api/Auth/login`

**Body:**
```json
{
  "username": "admin",
  "password": "tu_password"
}
```

**Respuesta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Usar el Token

En Swagger:
1. Click en el botón **"Authorize"**
2. Ingresa: `Bearer {tu_token}`
3. Click en **"Authorize"**

En Postman:
1. Tab **"Authorization"**
2. Type: **"Bearer Token"**
3. Token: `{tu_token}`

## Endpoints Disponibles

### Autenticación
- `POST /api/Auth/login` - Iniciar sesión

### Clientes
- `GET /api/Clientes` - Listar clientes
- `POST /api/Clientes` - Crear cliente
- `GET /api/Clientes/{id}` - Obtener cliente por ID
- `PUT /api/Clientes/{id}` - Actualizar cliente
- `DELETE /api/Clientes/{id}` - Eliminar cliente

### Productos
- `GET /api/productos` - Listar productos
- `POST /api/productos` - Crear producto
- `GET /api/productos/{id}` - Obtener producto por ID
- `PUT /api/productos/{id}` - Actualizar producto
- `DELETE /api/productos/{id}` - Eliminar producto

### Ventas
- `GET /api/Ventas` - Listar ventas
- `POST /api/Ventas` - Crear venta
- `GET /api/Ventas/{id}` - Obtener venta por ID
- `DELETE /api/Ventas/{id}` - Eliminar venta

## Pruebas con Postman

### Importar Colección

1. Abre Postman
2. Click en **"Import"**
3. Selecciona el archivo: `Documentacion/Postman/coleccion-sistema-ventas.json`
4. Configura el entorno con la variable `baseUrl`: `http://localhost:5031`

### Orden de Ejecución

1. **Login** - Genera el token automáticamente
2. **Crear Cliente**
3. **Crear Productos**
4. **Crear Venta** (usando IDs anteriores)

Ver más detalles en: [Documentacion/Postman/README.md](Documentacion/Postman/README.md)

## Estructura del Proyecto
```
SistemaVentas/
├── Controllers/          # Controladores de la API
├── Models/              # Modelos de datos
├── Data/                # Contexto de EF Core
├── Middleware/          # Middleware personalizado
├── Validators/          # Validadores FluentValidation
├── Documentacion/       # Screenshots y colecciones Postman
│   ├── Postman/
│   └── Swagger/
└── Program.cs           # Punto de entrada
```

## Modelos de Datos

### Cliente
```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "email": "juan@example.com"
}
```

### Producto
```json
{
  "id": 1,
  "nombre": "Laptop Dell",
  "precio": 1500.00,
  "stock": 10
}
```

### Venta
```json
{
  "id": 1,
  "fecha": "2026-02-12T22:00:00",
  "clienteId": 1,
  "total": 1515.00,
  "detalles": [
    {
      "productoId": 1,
      "cantidad": 1,
      "precioUnitario": 1500.00,
      "subtotal": 1500.00
    }
  ]
}
```

## Características de Seguridad

- Autenticación JWT
- Validación de datos con FluentValidation
- Manejo global de excepciones
- Logging con Serilog

## Capturas de Pantalla

Ver capturas de las pruebas en: `Documentacion/`

## Autor

**Matias Meza Sosa**
- Email: mmeza@freelancers.com.py

## Licencia

Este proyecto fue desarrollado como prueba técnica.

---

## Comandos Útiles
```bash
# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run

# Crear migración
dotnet ef migrations add NombreMigracion

# Aplicar migraciones
dotnet ef database update

# Listar paquetes
dotnet list package
```

## Troubleshooting

### Error de conexión a Oracle
Verifica que la cadena de conexión en `appsettings.json` sea correcta.

### Error 401 Unauthorized
Asegúrate de:
1. Haber ejecutado el endpoint de Login
2. Incluir el token en el header: `Authorization: Bearer {token}`

### Error al compilar
```bash
dotnet clean
dotnet restore
dotnet build
```