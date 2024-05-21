<script lang="ts">
  import type { TranslationApiResponse } from "$lib/translations/translation-api";
  import DeleteTranslationRowButton from "./DeleteTranslationRowButton.svelte";
  import TextCell from "./TextCell.svelte";

  const { translation, rowIndex }: {
    translation: TranslationApiResponse;
    rowIndex: number;
  } = $props();
</script>

{#snippet tableCell(value)}
<td class="text-center">{value}</td>
{/snippet}

{#snippet textCell(text)}
<TextCell {text} />
{/snippet}

<tr>
  {@render tableCell(rowIndex + 1)}
  {@render tableCell(translation.originText.language)}
  {@render tableCell(translation.translationText.language)}
  {@render tableCell(textCell(translation.originText))}
  {@render tableCell(textCell(translation.translationText))}
  <td class="text-center">
    <DeleteTranslationRowButton translationId={translation.id} />
  </td>
</tr>
