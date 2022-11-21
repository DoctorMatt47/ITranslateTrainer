import React from "react";
import "./Option.scss";

interface QuizOptionProps extends React.HTMLAttributes<HTMLLabelElement> {
  text: string;
  type: OptionType
}

export enum OptionType {
  Unknown,
  CorrectNotChosen,
  CorrectChosen,
  IncorrectChosen,
}

const Option = (props: QuizOptionProps) => {
  return (
    <label {...props}
           className={["option", labelClassName(props.type), props.className].join(" ")}>
      <input name="option" type="radio"/>
      {props.text}
    </label>
  );
};

const labelClassName = (type: OptionType) => {
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

export default Option;
