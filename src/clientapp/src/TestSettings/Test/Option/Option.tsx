import React from "react";
import "./Option.scss";

export enum OptionType {
  Unknown,
  CorrectNotChosen,
  CorrectChosen,
  IncorrectChosen,
}

interface QuizOptionProps extends React.HTMLAttributes<HTMLLabelElement> {
  text: string;
  type?: OptionType;
  optionId: number;
}

export default function Option({ type, className, optionId, text, ...labelProps }: QuizOptionProps) {
  return (
    <label {...labelProps}
           className={["option", labelClassName(type), className].join(" ")}>
      <input className="visually-hidden"
             name="option"
             value={optionId}
             type="radio" />
      {text}
    </label>
  );
};

const labelClassName = (type?: OptionType) => {
  switch (type) {
    case OptionType.Unknown:
      return "";
    case OptionType.CorrectNotChosen:
      return "correct-not-chosen";
    case OptionType.CorrectChosen:
      return "correct-chosen";
    case OptionType.IncorrectChosen:
      return "incorrect-chosen";
  }
};
