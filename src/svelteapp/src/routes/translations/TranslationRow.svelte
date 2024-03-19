<script lang="ts">

  import type { TranslationResponse } from "$lib/services/translation-service";
  import DeleteTranslationRowButton from "./DeleteTranslationRowButton.svelte";

  export let translation: TranslationResponse;
  export let rowIndex: number;

  $: rowCells = [
    rowIndex + 1,
    translation.originText.language,
    translation.translationText.language,
    translation.originText.value,
    translation.translationText.value,
  ];
</script>

<tr>
  {#each rowCells as cell}
    <td class="text-center">{cell}</td>
  {/each}
  <td class="text-center">
    <DeleteTranslationRowButton translationId={translation.id} />
  </td>
</tr>
