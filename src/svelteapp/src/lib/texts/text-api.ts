import { z } from "zod";
import createApiInstance from "$lib/common/services/api-service";

export type TextApiRequest = {
  value: string;
  language: string;
};

export type PutTextApiRequest = {
  text: string;
};

export const textApiResponseSchema = z.object({
  id: z.number(),
  value: z.string(),
  language: z.string(),
});

export class TextApi {
  private api = createApiInstance("texts");

  async putText(id: number, request: PutTextApiRequest): Promise<void> {
    await this.api.put(id.toString(), request);
  }
}
