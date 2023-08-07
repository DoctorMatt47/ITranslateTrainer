import { DayResultResponse } from "../../../common/services/day-results-service";

interface DayResultProps {
  dayResult: DayResultResponse;
}

export default function DayResult({ dayResult }: DayResultProps) {
  return (
    <tr className="translation-row text-center">
      <td className="w-20">{dayResult.day}</td>
      <td className="w-20 text-success">{dayResult.correctCount}</td>
      <td className="w-30 text-danger">{dayResult.incorrectCount}</td>
      <td className="w-30">{correctPercent()}</td>
    </tr>
  );

  function correctPercent(): string {
    const count = dayResult.correctCount + dayResult.incorrectCount;
    return `${round(dayResult.correctCount / count)}%`;
  }

  function round(num : number) : number {
    return Math.round((num + Number.EPSILON) * 100);
  }
};
