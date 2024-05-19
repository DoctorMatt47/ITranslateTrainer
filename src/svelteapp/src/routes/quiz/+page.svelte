<script lang="ts">
  import { getContext } from "svelte";
  import { goto } from "$app/navigation";
  import QuizOption from "./QuizOption.svelte";
  import AppHeading from "$lib/common/components/AppHeading.svelte";
  import type { TestService } from "$lib/tests/test-service.svelte";
  import AppButton from "$lib/common/components/AppButton.svelte";

  const testService = getContext<TestService>("testService");

  $effect.pre(() => {
    console.log("effect pre");
    if (!testService.test) goto("/quiz/settings");
  });

  async function optionClick(optionId: number) {
    await testService.answerOnTest(optionId);
  }

  async function restart() {
    testService.resetTest();
    await goto("/quiz/settings");
  }

  async function next() {
    await testService.fetchTest();
  }

</script>

{#if testService.test}
  <AppHeading>{testService.test.text}</AppHeading>
  <div class="w-1/3 mx-auto">
    <div class="flex flex-col gap-8">
      <div class="grid grid-cols-none gap-4">
        {#each testService.test.options as option (option.id)}
          <QuizOption {option} onclick={() => optionClick(option.id)} />
        {/each}
      </div>
      <div class="grid grid-cols-2 gap-6">
        <AppButton onclick={restart}>Restart</AppButton>
        <AppButton onclick={next}>Next</AppButton>
      </div>
    </div>
  </div>
{:else}
  <AppHeading>Loading...</AppHeading>
{/if}
