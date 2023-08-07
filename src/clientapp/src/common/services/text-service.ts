import { TextResponse } from "./translation-service";
import { api } from "./api-service";

export async function getTexts(): Promise<TextResponse[]> {
  const response = await api.get("/api/texts");
  return response.data;
}
