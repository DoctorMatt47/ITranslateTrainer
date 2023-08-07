import { TextResponse } from "../../../common/services/translation-service";

interface TextResultProps {
  textResult: TextResponse;
}

const TextResult = ({textResult}: TextResultProps) => {
  return (
    <tr className="translation-row text-center">
      <td className="w-20">{textResult.position}</td>
      <td className="w-20">{textResult.string}</td>
      <td className="w-20 text-success">{textResult.correctCount}</td>
      <td className="w-20 text-danger">{textResult.incorrectCount}</td>
      <td className="w-20">{correctPercent()}</td>
    </tr>
  );

  function correctPercent() : string {
    const count = textResult.correctCount + textResult.incorrectCount;
    return `${round(textResult.correctCount / count)}%`;
  }

  function round(num : number) : number {
    return Math.round((num + Number.EPSILON) * 100);
  }
};

export default TextResult;
