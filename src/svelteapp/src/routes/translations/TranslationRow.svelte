<script lang="ts">
  import type { TranslationApiResponse } from "$lib/translations/translation-api";
  import DeleteTranslationRowButton from "./DeleteTranslationRowButton.svelte";

  const { translation, rowIndex }: {
    translation: TranslationApiResponse;
    rowIndex: number;
  } = $props();

  const rowCells = $derived([
    rowIndex + 1,
    translation.originText.language,
    translation.translationText.language,
    translation.originText.value,
    translation.translationText.value,
  ]);
</script>

<tr>
  {#each rowCells as cell}
    <td class="text-center">{cell}</td>
  {/each}
  <td class="text-center">
    <DeleteTranslationRowButton translationId={translation.id} />
  </td>
</tr>
