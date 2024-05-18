import createApiInstance from "$lib/common/services/api-service";
import { z } from "zod";

export type PutTestApiRequest = {
  fromLanguage: string;
  toLanguage: string;
  optionCount: number;
};

export type AnswerOnTestApiRequest = {
  optionId: number;
};

export const optionApiResponseSchema = z.object({
  id: z.number(),
  text: z.string(),
});

export const testApiResponseSchema = z.object({
  id: z.number(),
  text: z.string(),
  answerTime: z.string(),
  options: z.array(optionApiResponseSchema),
});

export const answerOnTestApiResponseSchema = z.object({
  correctOptionId: z.number(),
});

export type TestApiResponse = z.infer<typeof testApiResponseSchema>;

export type OptionApiResponse = z.infer<typeof optionApiResponseSchema>;

export type AnswerOnTestApiResponse = z.infer<typeof answerOnTestApiResponseSchema>;

export class TestApi {
  private api = createApiInstance("tests");

  async getTests(): Promise<TestApiResponse[]> {
    const response = await this.api.get("");
    return z.array(testApiResponseSchema).parse(response.data);
  }

  async getTest(id: number): Promise<TestApiResponse> {
    const response = await this.api.get(id.toString());
    return testApiResponseSchema.parse(response.data);
  }

  async putTest(request: PutTestApiRequest): Promise<TestApiResponse> {
    const response = await this.api.put("", request);
    return testApiResponseSchema.parse(response.data);
  }

  async answerOnTest(id: number, request: AnswerOnTestApiRequest): Promise<AnswerOnTestApiResponse> {
    const response = await this.api.put(`${id}/answer`, request);
    return answerOnTestApiResponseSchema.parse(response.data);
  }
}
