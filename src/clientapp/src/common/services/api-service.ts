import axios from "axios";
import { baseUrl } from "../utils/env";

export const api = axios.create({
  baseURL: baseUrl + "/api",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});
