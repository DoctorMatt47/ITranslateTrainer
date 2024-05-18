import { z } from "zod";
import createApiInstance from "$lib/common/services/api-service";

export type TextApiRequest = {
  value: string;
  language: string;
};

export const textApiResponseSchema = z.object({
  id: z.number(),
  value: z.string(),
  language: z.string(),
});

export type TextApiResponse = z.infer<typeof textApiResponseSchema>;

export class TextsApi {
  private api = createApiInstance("texts");

  async getTexts(): Promise<TextApiResponse[]> {
    const response = await this.api.get("/api/texts");
    return z.array(textApiResponseSchema).parse(response.data);
  }
}
