export var baseUrl: string;

switch (process.env.NODE_ENV) {
  case "production":
    baseUrl = "http://192.168.0.102:5207";
    break;
  default:
    baseUrl = "http://localhost:5207";
}
